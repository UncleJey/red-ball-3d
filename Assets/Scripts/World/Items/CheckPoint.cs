using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour 
{
	public int CheckNo = 0;
	public bool finalDoor = false;

	public void check()
	{
		GetComponent<Collider2D> ().enabled = false;

		ParticleSystem ps = GetComponentInChildren<ParticleSystem> ();
		if (ps)
			ps.Play ();
	}
}
