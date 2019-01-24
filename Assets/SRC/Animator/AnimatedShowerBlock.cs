using UnityEngine;
using System.Collections;

public class AnimatedShowerBlock : MonoBehaviour 
{
	Vector3 InitPos;
	static bool myTurn = true;

	void Start () 
	{
		InitPos = transform.localPosition;
		transform.localPosition += Vector3.down * 8;
		transform.localScale = Vector3.zero;

		StartCoroutine (scaler ());
	}

	void OnDisable()
	{
		StopAllCoroutines ();
		transform.localPosition = InitPos;
		transform.localScale = Vector3.one;
	}

	IEnumerator scaler()
	{
		float tm = Time.deltaTime;

		while (!myTurn)
			yield return null;

		MusicProc.doEvent (ActionEvent.ShowBlock);

		myTurn = false;
		yield return new WaitForSeconds(0.1f);
		myTurn = true;
		while (Vector3.SqrMagnitude(transform.localPosition - InitPos) > 0.01f)
		{
			yield return null;
			tm = Time.deltaTime * 10;
			transform.localScale = Vector3.MoveTowards (transform.localScale, Vector3.one, tm / 4);
			transform.localPosition = Vector3.MoveTowards (transform.localPosition, InitPos, tm);
		}
		transform.localScale = Vector3.one;
		transform.localPosition = InitPos;
	}
	
}
