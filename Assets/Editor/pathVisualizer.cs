using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor( typeof( enemy ) )]
public class pathVisualizer : Editor 
{
	public float circleSize = 1;
	
	void OnSceneGUI () 
	{
		enemy e = target as enemy;
		if (e != null)
		{
			Handles.color = Color.red;
			if (e.canMove == movement.BOTH)
				Handles.CircleHandleCap(0,
				                  e.transform.position + new Vector3(0,0.5f,0),
		                  Camera.main.transform.rotation, 
		                  e.moveRadius, EventType.Ignore);
			else if (e.canMove == movement.HORISONTAL)
			{

				Handles.DrawLine(e.transform.position + new Vector3(-e.moveRadius / 2,0.5f,0),  e.transform.position + new Vector3(e.moveRadius / 2,0.5f,0));
			}
		}
	}
}