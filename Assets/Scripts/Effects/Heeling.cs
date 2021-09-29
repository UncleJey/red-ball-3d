using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[AddComponentMenu("UI/Effects/Heeling")]
public class Heeling : BaseMeshEffect
{
	public Vector2 heelX,heelY = Vector2.zero;

	public override void ModifyMesh(VertexHelper helper)
	{
		if (!IsActive() || helper.currentVertCount == 0)
			return;

		List<UIVertex> vertices = new List<UIVertex>();
		helper.GetUIVertexStream(vertices);

		Vector3 center = Vector2.zero;

		for (int i = vertices.Count-1; i>=0 ; i--)
			center += vertices [i].position;

		if (vertices.Count > 0)
			center = center / vertices.Count;

		UIVertex v = new UIVertex();
		for (int i = 0; i < helper.currentVertCount; i++)
		{
			helper.PopulateUIVertex(ref v, i);
			if (v.position.x > center.x)
				v.position.x += heelX.x *  v.position.y;
			else
				v.position.x -= heelX.y * v.position.y;

			if (v.position.y > center.y)
				v.position.y += heelY.x *  v.position.x;
			else
				v.position.y -= heelY.y * v.position.x;


			helper.SetUIVertex(v, i);
		}
	}
}