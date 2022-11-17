using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

// https://math.stackexchange.com/questions/3557767/how-to-construct-a-catenary-of-a-specified-length-through-two-specified-points

public class HangingWire : MonoBehaviour
{
    public float L = 20;
    public Material material;

    public void SetShader()
    {
        Vector3 right = transform.right;
        Vector3 xAxis = new Vector3(right.x, 0, right.z);

        Vector2 x = new Vector2(right.x, right.z);
        x = new Vector2(x.magnitude, right.y);

        Vector2 y = new Vector2(-x.y, x.x);

        Vector2 s = transform.lossyScale / 2;

        Vector2 p1 = x * s.x + y * s.y;
        Vector2 p2 = x * -s.x + y * s.y;


        Vector2 d = p1 - p2;

        if (L*L <= Vector2.Dot(d, d) + 1)
            L = Mathf.Sqrt(Vector2.Dot(d, d) + 1);

        float A = SolveR(Mathf.Sqrt(L*L - d.y*d.y) / d.x);
        
        float a = d.x / (2 * A);
        float b = (p1.x + p2.x) / 2 - a * MathF.Atanh(d.y / L);
        float c = p1.y - a * MathF.Cosh((p1.x - b) / a);

        material.SetFloat("_a", a);
        material.SetFloat("_b", b);
        material.SetFloat("_c", c);


        right.y = 0;
        material.SetVector("_right", right.normalized);
    }

    public float SolveR(float r)
    {
        float A = r < 3
            ? Mathf.Sqrt(6 * (r - 1))
            : Mathf.Log(2 * r) + Mathf.Log(Mathf.Log(2 * r));

        // Newton iteration
        for (int i = 0; i < 5; i++)
            A -= (MathF.Sinh(A) - r * A) / (MathF.Cosh(A) - r);

        return A;
    }
}

[CustomEditor(typeof(HangingWire))]
public class HangingWireEditor : Editor
{
    HangingWire hw;

    private void OnEnable()
    {
        hw = (HangingWire)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        //if (GUILayout.Button("Update shader"))
        hw.SetShader();
    }
}
