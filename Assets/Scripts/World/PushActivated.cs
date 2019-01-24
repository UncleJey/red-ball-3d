using UnityEngine;
using System.Collections;

public class PushActivated : MonoBehaviour 
{
	public ActivatorBase[] actionObjects;
	public bool shutDownKinematic = false;

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (GetComponent<Rigidbody2D> ().isKinematic)
		{
			if (shutDownKinematic)
				GetComponent<Rigidbody2D> ().isKinematic = false;

			foreach (ActivatorBase ab in actionObjects)
			{
				if (ab)
					ab.doAction();
			}
		}
	}
}
