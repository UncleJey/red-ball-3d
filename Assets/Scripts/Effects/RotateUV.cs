using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Effects/RotateUV")]
public class RotateUV : BaseMeshEffect
{
    [SerializeField]
    private float angle = 0f;

    public override void ModifyMesh(VertexHelper helper)
    {
        if (!IsActive() || helper.currentVertCount == 0)
            return;

        List<UIVertex> vertices = new List<UIVertex>();
        helper.GetUIVertexStream(vertices);


        float uMin = 1f, uMax = 0f, vMin = 1f, vMax = 0f;

        for (int i = vertices.Count - 1; i >= 0; i--)
        {
            Vector2 uv = vertices[i].uv0;

            uMin = Mathf.Min(uv.x, uMin);
            vMin = Mathf.Min(uv.y, vMin);

            uMax = Mathf.Max(uv.x, uMax);
            vMax = Mathf.Max(uv.y, vMax);
        }

        Vector2 mid = new Vector2(uMin + uMax, vMin + vMax) * 0.5f;

        Vector2 hand = new Vector2(uMax - mid.x, vMax - mid.y);

        UIVertex v = new UIVertex();

        for (int i = 0; i < helper.currentVertCount; i++)
        {
            helper.PopulateUIVertex(ref v, i);

            var dif = ((Vector2)v.uv0 - mid);
            float ang = (Vector2.SignedAngle(Vector2.right, dif) + angle) * Mathf.Deg2Rad;

            v.uv0 = mid + Vector2.Scale(hand, new Vector2(Mathf.Cos(ang), Mathf.Sin(ang)));

            helper.SetUIVertex(v, i);
        }
    }
}