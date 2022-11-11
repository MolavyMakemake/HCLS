using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SecondOrderDynamics
{
    public float F;
    public float Z;
    public float R;

    Vector3 xp;
    Vector3 y, yd;
    float k1, k2, k3;
    
    public SecondOrderDynamics(float f, float z, float r, Vector3 x0)
    {
        // Compute constants
        float c = 2 * Mathf.PI * f;
        k1 = 2 * z / (Mathf.PI * f);
        k2 = 1 / (c * c);
        k3 = r * z / c;

        // Initialize vars
        xp = x0;
        y = x0;
        yd = Vector3.zero;
    }

    public Vector3 Update(float T, Vector3 x)
    {
        Vector3 xd = (x - xp) / T;
        xp = x;

        y = y + T * yd;
        yd = yd + T * (x + k3 * xd - y - k1 * yd) / k2;
        return y;
    }
}
