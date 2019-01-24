using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SnapToGrid : MonoBehaviour 
{
	public bool snap = true;

#if UNITY_EDITOR
	// Update is called once per frame
	void Update () 
	{
		if (snap && UnityEditor.Selection.activeGameObject != null && UnityEditor.Selection.activeGameObject.GetComponent<Block>())
		{
			UnityEditor.Selection.activeGameObject.transform.localPosition = new Vector3 (
				  ((int)(UnityEditor.Selection.activeGameObject.transform.localPosition.x / 4)) * 4
				, ((int)(UnityEditor.Selection.activeGameObject.transform.localPosition.y / 4)) * 4
				, ((int)(UnityEditor.Selection.activeGameObject.transform.localPosition.z / 4)) * 4
				);
		}
	}
#endif
}
