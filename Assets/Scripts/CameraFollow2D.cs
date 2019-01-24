using UnityEngine;
using System.Collections;

public class CameraFollow2D : MonoBehaviour 
{
	public float dampTime = 0.3f; //offset from the viewport center to fix damping
	Vector3 velocity = Vector3.zero;
	public Transform target;

	public Material bgMaterial;
	Vector2 offset = Vector2.zero;
	public float transformDiv = 6f;
	Vector3 exPos;
	static CameraFollow2D instance;

	static float ShakeIntensity = 0;

	public static void shakeIt()
	{
		ShakeIntensity = 0.01f;
	}

	public static void stopShake()
	{
		ShakeIntensity = 0f;
		instance.transform.rotation = instance.OriginalRot;
	}

	public static void changeTarget(Transform pNewTarget = null)
	{
		instance.target = pNewTarget??instance._origTarget;
	}

	void Awake()
	{
		instance = this;
	}

	Quaternion OriginalRot;
	Transform _origTarget;
	void Start()
	{
		OriginalRot = transform.rotation;
		_origTarget = target;
	}

	void LateUpdate() 
	{
		if(target) 
		{
			Vector3 point = Camera.main.WorldToViewportPoint(target.position);
			Vector3 delta = target.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.3f, point.z));
			Vector3 destination = transform.position + delta;
			
			// Set this to the Y position you want the camera locked to
			exPos = transform.position;
			
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);

			offset.y -= (exPos.y - transform.position.y)/transformDiv;
			offset.x -= (exPos.x - transform.position.x)/transformDiv;
			bgMaterial.SetTextureOffset("_MainTex", offset);
		}

		if(ShakeIntensity > 0)
		{
			transform.position += Random.insideUnitSphere * ShakeIntensity;
			transform.rotation = new Quaternion(OriginalRot.x + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f,
			                                    OriginalRot.y + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f,
			                                    OriginalRot.z + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f,
			                                    OriginalRot.w + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f);
			
			ShakeIntensity -= Time.deltaTime / 10;
			if (ShakeIntensity<=0)
				stopShake();
		}

	}
}
