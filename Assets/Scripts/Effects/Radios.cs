using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[AddComponentMenu("UI/Effects/Radios")]
public class Radios : BaseMeshEffect
{
	public float radios = 15f;
	public float addX = -0.4f;

	public override void ModifyMesh(VertexHelper helper)
	{
		if (!IsActive() || helper.currentVertCount == 0)
			return;

		List<UIVertex> vertices = new List<UIVertex>();
		helper.GetUIVertexStream(vertices);

//		float bottomY = vertices[0].position.y;
//		float topY = vertices[0].position.y;

//		float minX = vertices[0].position.x;
		float maxX = vertices[0].position.x;

		for (int i = vertices.Count-1; i>=0 ; i--)
		{
//			float y = vertices[i].position.y;
			float x = vertices[i].position.x;
//			if (y > topY)
//				topY = y;
//			else if (y < bottomY)
//				bottomY = y;

			if (x > maxX)
				maxX = x;
//			else if (x < minX)
//				minX = x;
		}

//		float uiElementHeight = topY - bottomY;
//		float midX = (maxX - minX) / 2 + minX;

		UIVertex v = new UIVertex();
//		Debug.Log (helper.currentVertCount); на 17 букв получалось 76 вершин
		for (int i = 0; i < helper.currentVertCount; i++)
		{
			helper.PopulateUIVertex(ref v, i);
			v.position.y += Mathf.Sin ((maxX - v.position.x )/ maxX * 1.5f + addX) * radios ;
			
			helper.SetUIVertex(v, i);
		}
	}
}