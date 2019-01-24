using UnityEngine;
using System.Collections;

public class BlobBehaviour : MonoBehaviour 
{
	public LayerMask m_GroundLayer;
	public LayerMask lootMask;
	public static BlobBehaviour instance;

	JellySprite m_JellySprite;
//	Vector2 ph = -Vector2.up;
	float cx = 0;
	float maxAngle = 15f;
	float rotSpeed = 3;

	float gravity = 50;
	float lastJump = 0;

	int center = 0;

	void Awake()
	{
		instance = this;
	}

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () 
	{
		m_JellySprite = GetComponent<JellySprite>();
		center = Screen.width >> 1;
	}

	bool inJump = false;
	bool grounded = false;

	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update () 
	{
		grounded = m_JellySprite.IsGrounded (m_GroundLayer, 1);
		//speed = Vector2.SqrMagnitude ((( Vector2)transform.localPosition ) - exexPos);
		//exexPos = exPos;
		//exPos = transform.localPosition;

		if (lastJump > 0)
			lastJump -= Time.deltaTime;
		if (grounded)
		{
			if (inJump)
				MusicProc.doEvent(ActionEvent.Landed);
			inJump = false;
		}
		else
			inJump = true;

		bool left = false;
		bool right = false;

		foreach (Touch t in Input.touches)
		{
			if (t.position.x > center)
				right = true;
			else
				left = true;
		}

		if (Input.GetKey (KeyCode.RightArrow))
			right = true;

		if (Input.GetKey (KeyCode.LeftArrow))
			left = true;

		if (left && right)
		{
			if (lastJump<=0 && grounded)
			{
				//m_JellySprite.AddForce(Vector2.up * 10000);
				m_JellySprite.AddForce(Vector3.up * 10000);// - m_JellySprite.WhereIsGround2D(m_GroundLayer)*500);
				lastJump = 0.5f;
				MusicProc.doEvent(ActionEvent.Jump);
			}
		}
		else if (right)
		{
			//jumpVector += Vector2.right * 150;
			cx += rotSpeed * Time.deltaTime;
			if (cx > 1)
				cx = 1;
		}
		else if (left)
		{
			cx -= rotSpeed * Time.deltaTime;
			if (cx <-1)
				cx = -1;
		}
		else if (cx < 0)
		{
			cx += Time.deltaTime * rotSpeed;
			if (cx >=0)
				cx = 0;
		}
		else if (cx > 0)
		{
			cx -= Time.deltaTime * rotSpeed;
			if (cx <=0)
				cx = 0;
		}

		//Camera.main.transform.rotation = Quaternion.Euler(0, 0, cx * maxAngle);
		Physics2D.gravity = new Vector2(Mathf.Sin(cx) * gravity,- Mathf.Cos(cx) * gravity);

		if (Input.GetKeyDown (KeyCode.Escape))
			Application.LoadLevel ("menu");
	}

}