using UnityEngine;
using System.Collections;

public class WorldManager : MonoBehaviour 
{
	public static WorldManager instance;

	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		ActivatorBase[] bases = GetComponentsInChildren<ActivatorBase> (true);
		foreach (ActivatorBase b in bases)
			b.Init ();
	}
}
