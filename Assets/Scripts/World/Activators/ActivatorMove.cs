using UnityEngine;
using System.Collections;

public class ActivatorMove : ActivatorBase
{
	public Vector3 moveFrom;
	public Vector3 moveTo;
	public float moveSpeed = 5f;
	public float pauseBetweenActions = 1f;
	public int moveCount = 1;

	public bool shakeCamera = true;
	public Transform switchCameraTo;
	public bool enableControl = false;

	public override void Init ()
	{
		transform.localPosition = moveFrom;
	}

	//iWorldAction
	public override void doAction()
	{
		if (!moving && moveCount>0)
		{
			Debug.Log("start");
			moving = true;
			moveCount--;
			StartCoroutine(mover());
			if (switchCameraTo != null)
				CameraFollow2D.changeTarget (switchCameraTo);
		}
	}

	void OnDisable()
	{
		StopAllCoroutines ();
		if (moving)
			stopAction ();
	}

	void stopAction()
	{
		transform.localPosition = moveTo;
		moving = false;
		if (shakeCamera)
			CameraFollow2D.stopShake();
		if (switchCameraTo != null)
			CameraFollow2D.changeTarget ();
	}

	bool moving = false;
	IEnumerator mover()
	{
		yield return new WaitForSeconds(pauseBetweenActions);
		while (Vector3.SqrMagnitude(moveTo - transform.localPosition)>0.01f)
		{
			yield return null;
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, moveTo, Time.deltaTime * moveSpeed);
			if (shakeCamera)
				CameraFollow2D.shakeIt();
		}
		if (shakeCamera)
			CameraFollow2D.stopShake();
		yield return new WaitForSeconds(pauseBetweenActions);
		stopAction ();
	}
}
