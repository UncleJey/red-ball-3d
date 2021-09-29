using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[AddComponentMenu("UI/Effects/Radial")]
public class Radial : BaseMeshEffect
{
    public int Segments = 5;
    public int ExtraSizeSegments = 0;

    public bool RectangleEdges = false;

    [Range(1f, 360f)]
    public float AngleStart = 0f;
    [Range(1f, 360f)]
    public float Angle = 45f;

    [Range(0f, 1f)]
    public float Offset = 0f;

    public float UVPower = 1f;

    public bool Normalize = false;

    [SerializeField, HideInInspector]
    private Vector2[] normalizedUV = new Vector2[0];

    [SerializeField, HideInInspector]
    private Vector2[] normalizedPos = new Vector2[0];

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        normalizedPos = new Vector2[(Segments + (ExtraSizeSegments > 1? ExtraSizeSegments: 1)) * 2];
        normalizedUV = new Vector2[normalizedPos.Length];

        Vector3 center = new Vector3(0.5f, 0.5f, 0f);
        Vector3 size = new Vector3(0.5f, 0.5f);

        Vector2 uvSize = new Vector2(1f,1f);
        Vector2 uvMid = new Vector2(0.5f, 0.5f);

        bool usePow = UVPower > 0.1f;

        int index = 0;

        int extras = ExtraSizeSegments > 0 ? ExtraSizeSegments : 1;
        for (int i = 0; i <= Segments; i++)
        {
            int divide = ((i == 0 || i == Segments)) ? extras : 1;
            float part = 0;
            while (part < divide)
            {
                float radPos = (i + part / divide) / (Segments + 1f - 1f / extras);
                float v = radPos;
                if (usePow)
                    v = Mathf.Pow(Mathf.Abs(v - uvMid.y) * 2f / uvSize.y, UVPower) * Mathf.Sign(v) * uvSize.y * 0.5f + uvMid.y;

                Vector3 fullRad = Vector3.Scale(GetOffset(radPos), size);

                Vector3 bottom;
                Vector3 top = center + fullRad;

                if (RectangleEdges && (i == 0 || i == Segments))
                    bottom = top - Vector3.Scale(GetOffset((i + (i == 0 ? 1 : -1)) / ((float) Segments)), size) * (1f - Offset);
                else
                    bottom = center + fullRad * Offset;

                normalizedPos[index] = bottom;
                normalizedUV[index++] = new Vector2(1f, v);

                normalizedPos[index] = top;
                normalizedUV[index++] = new Vector2(0f, v);

                part++;
            }
        }

        if (Normalize)
        {
            var xMin = float.MaxValue;
            var xMax = float.MinValue;
            var yMin = float.MaxValue;
            var yMax = float.MinValue;


            for (int i = normalizedPos.Length - 1; i >= 0; i--)
            {
                Vector2 pos = normalizedPos[i];
                xMin = Mathf.Min(pos.x, xMin);
                yMin = Mathf.Min(pos.y, yMin);

                xMax = Mathf.Max(pos.x, xMax);
                yMax = Mathf.Max(pos.y, yMax);
            }

            Vector3 finalSize = new Vector3(xMax - xMin, yMax - yMin);
            Vector3 scale = new Vector3(2f * size.x / finalSize.x, 2f * size.y / finalSize.y, 1f);

            for (int i = normalizedPos.Length - 1; i >= 0; i--)
                normalizedPos[i] = Vector3.Scale(normalizedPos[i] - new Vector2(xMin, yMin), scale);
        }

        base.OnValidate();
    }
#endif

    private static Vector2 UVLerp(Vector2 _min, Vector2 _size, Vector2 _normalized)
    {
        return _min + Vector2.Scale(_normalized, _size);
    }

    public override void ModifyMesh(VertexHelper _helper)
    {
        if (!IsActive() || _helper.currentVertCount == 0)
            return;

        List<UIVertex> vertices = new List<UIVertex>();
        _helper.GetUIVertexStream(vertices);

        float uMin = 1f, uMax = 0f, vMin = 1f, vMax = 0f;
        float xMin = float.MaxValue, xMax = float.MinValue, yMin = float.MaxValue, yMax = float.MinValue;

        Color32 white = vertices[0].color;
        UIVertex vert;

        for (int i = vertices.Count - 1; i >= 0; i--)
        {
            vert = vertices[i];
            var uv = vert.uv0;
            var pos = vert.position;

            uMin = Mathf.Min(uv.x, uMin);
            vMin = Mathf.Min(uv.y, vMin);

            uMax = Mathf.Max(uv.x, uMax);
            vMax = Mathf.Max(uv.y, vMax);

            xMin = Mathf.Min(pos.x, xMin);
            yMin = Mathf.Min(pos.y, yMin);

            xMax = Mathf.Max(pos.x, xMax);
            yMax = Mathf.Max(pos.y, yMax);
        }

        _helper.Clear();

        Vector3 center = new Vector3((xMin), (yMin), 0f);
        Vector3 size = new Vector3(xMax - xMin, yMax - yMin);

        var uvMin = new Vector2(uMin, vMin);
        var uvSize = new Vector2(uMax, vMax) - uvMin;

        for (int i = 0; i < normalizedPos.Length; i+= 2)
        {
            _helper.AddVert(center + Vector3.Scale(size, normalizedPos[i]), white, UVLerp(uvMin, uvSize, normalizedUV[i]));
            _helper.AddVert(center + Vector3.Scale(size, normalizedPos[i + 1]), white, UVLerp(uvMin, uvSize, normalizedUV[i + 1]));

            if (i > 0)
            {
                int cnt = _helper.currentVertCount - 1;
                _helper.AddTriangle(cnt - 2, cnt - 1, cnt);
                _helper.AddTriangle(cnt - 1, cnt - 2, cnt - 3);
            }
        }


        /*if (!IsActive() || _helper.currentVertCount == 0)
            return;

        List<UIVertex> vertices = new List<UIVertex>();
        _helper.GetUIVertexStream(vertices);

        float uMin = 1f, uMax = 0f, vMin = 1f, vMax = 0f;
        float xMin = float.MaxValue, xMax = float.MinValue, yMin = float.MaxValue, yMax = float.MinValue;

        Color32 white = vertices[0].color;
        UIVertex vert;

        for (int i = vertices.Count - 1; i >= 0; i--)
        {
            vert = vertices[i];
            var uv = vert.uv0;
            var pos = vert.position;

            uMin = Mathf.Min(uv.x, uMin);
            vMin = Mathf.Min(uv.y, vMin);

            uMax = Mathf.Max(uv.x, uMax);
            vMax = Mathf.Max(uv.y, vMax);

            xMin = Mathf.Min(pos.x, xMin);
            yMin = Mathf.Min(pos.y, yMin);

            xMax = Mathf.Max(pos.x, xMax);
            yMax = Mathf.Max(pos.y, yMax);
        }

        _helper.Clear();
        
        Vector3 center = new Vector3((xMin), (yMin), 0f);
        Vector3 size = new Vector3(xMax - xMin, yMax - yMin);

        Vector2 uvSize = new Vector2(uMax - uMin, vMax - vMin);
        Vector2 uvMid = new Vector2(uMin + uMax, vMin + vMax) * 0.5f;

        var usePow = UVPower > 0.1f;



        int extras = ExtraSizeSegments > 0? ExtraSizeSegments: 1;
        for (int i = 0; i <= Segments; i++)
        {
            int divide = ((i == 0 || i == Segments)) ? extras : 1;
            float part = 0;
            while (part < divide)
            {
                float radPos = (i + part / divide) / (Segments + 1f - 1f / extras);
                float v = vMin + (vMax - vMin) * radPos;
                if (usePow)
                    v = Mathf.Pow(Mathf.Abs(v - uvMid.y) * 2f / uvSize.y, UVPower) * Mathf.Sign(v) * uvSize.y * 0.5f + uvMid.y;

                Vector3 fullRad = Vector3.Scale(GetOffset(radPos), size);

                Vector3 bottom;
                Vector3 top = center + fullRad;

                if (RectangleEdges && (i == 0 || i == Segments))
                {
                    bottom = top - Vector3.Scale(GetOffset((i +(i == 0? 1 :-1)) / ((float)Segments)), size) * (1f - Offset);
                }
                else
                {
                    bottom = center + fullRad * Offset;
                }

                _helper.AddVert(bottom, white, new Vector2(uMax, v));
                _helper.AddVert(top, white, new Vector2(uMin, v));

                if (i > 0 || part > 0)
                {
                    int cnt = _helper.currentVertCount - 1;
                    _helper.AddTriangle(cnt - 2, cnt - 1, cnt);
                    _helper.AddTriangle(cnt - 1, cnt - 2, cnt - 3);
                }

                part++;
            }
        }

        if (!Normalize)
            return;

        xMin = float.MaxValue;
        xMax = float.MinValue;
        yMin = float.MaxValue;
        yMax = float.MinValue;

        vert = new UIVertex();

        for (int i = _helper.currentVertCount - 1; i >= 0; i--)
        {
            _helper.PopulateUIVertex(ref vert, i);
            var pos = vert.position;

            xMin = Mathf.Min(pos.x, xMin);
            yMin = Mathf.Min(pos.y, yMin);

            xMax = Mathf.Max(pos.x, xMax);
            yMax = Mathf.Max(pos.y, yMax);
        }

        Vector3 finalSize = new Vector3(xMax - xMin, yMax - yMin);
        Vector3 scale = new Vector3(size.x / finalSize.x, size.y / finalSize.y, 1f);

        for (int i = _helper.currentVertCount - 1; i >= 0; i--)
        {
            _helper.PopulateUIVertex(ref vert, i);
            vert.position = Vector3.Scale(vert.position - new Vector3(xMin, yMin), scale) + center;

            _helper.SetUIVertex(vert, i);
        }*/
    }

    private Vector3 GetOffset(float _radPos)
    {
        float angle = ( AngleStart + Angle * _radPos) * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f);
    }
}