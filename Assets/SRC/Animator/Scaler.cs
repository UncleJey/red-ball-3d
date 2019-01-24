using UnityEngine;
using System.Collections;

public class Scaler : AnimationBase
{
	public Vector3 scaleTo = new Vector3(1.1f, 1.1f, 1.1f);
	public float forwSpeed = 1f;
	public float backSpeed = 1f;

	Vector3 scale = Vector3.one;

	public override void Animate () 
	{
		StopAllCoroutines ();
		StartCoroutine (scaler ());
	}
	/*
	float t = 0.5f;
	void Update()
	{
		t -= Time.deltaTime;
		if (t < 0) 
		{
			t = 0.5f;
			if (Vector3.SqrMagnitude (scale - Vector3.one) == 0)
				Animate ();
		}
	}
*/
	IEnumerator scaler()
	{
		scale = Vector3.one;
		yield return null;

		while (Vector3.SqrMagnitude(scale - scaleTo) > 0.01f)
		{
			yield return null;
			scale = Vector3.MoveTowards(scale, scaleTo, Time.deltaTime * forwSpeed);
			transform.localScale = scale;
		}

		while (Vector3.SqrMagnitude(scale - Vector3.one) > 0.01f)
		{
			yield return null;
			scale = Vector3.MoveTowards(scale, Vector3.one, Time.deltaTime * forwSpeed);
			transform.localScale = scale;
		}

		transform.localScale = Vector3.one;
	}
	
}
