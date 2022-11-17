using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ProcArms : MonoBehaviour
{
    public Transform upper;
    public Transform lower;
    public Transform hand;

    public Transform target;

    public float minAngle = 0;
    public float maxAngle = Mathf.PI - 0.5f;

    public Sprite3D spriteUpper;
    public Sprite3D spriteLower;

    Vector2 Position(float theta, float l)
    {
        return new Vector2(Mathf.Cos(theta), Mathf.Sin(theta)) * l;
    }

    private void FixedUpdate()
    {
        UpdateJoints();
    }

    public void UpdateJoints()
    {
        Vector2 p0 = upper.localPosition;
        Vector2 p1 = lower.localPosition;
        Vector2 p2 = hand.localPosition;

        Vector2 toTarget = (Vector2)transform.localPosition - p0;

        float a = Vector2.Distance(p0, p1);
        float b = Vector2.Distance(p1, p2);
        float c = toTarget.magnitude;

        if (c > a + b - 1e-10f)
            c = a + b - 1e-10f;

        float B = Mathf.Acos((a * a + c * c - b * b) / (2 * a * c));
        float C = Mathf.Acos((a * a + b * b - c * c) / (2 * a * b));

        if (C < minAngle)
            C = minAngle;

        else if (C > maxAngle)
        {
            B -= (maxAngle - C) * 0.6f;
            C = maxAngle;
        }

        if (float.IsNaN(B) || float.IsNaN(C))
        {
            Debug.Log("Invalid triangle " + a + ", " + b + ", " + c);
            return;
        }

        float theta = Mathf.Atan2(toTarget.y, toTarget.x);

        p1 = p0 + Position(theta - B, a);
        p2 = p1 + Position(theta - B + (Mathf.PI - C), b);

        lower.localPosition = p1;
        hand.localPosition = p2;


        if (Application.isPlaying && spriteUpper != null)
        {
            spriteUpper.rotY = Mathf.Rad2Deg * Mathf.Atan2(p0.x - p1.x, p1.y - p0.y);
            upper.rotation = Quaternion.identity;
        }
        else
            upper.rotation = RotationX(p1 - p0);

        if (Application.isPlaying && spriteLower != null)
        {
            spriteLower.rotY = Mathf.Rad2Deg * Mathf.Atan2(p1.x - p2.x, p2.y - p1.y);
            lower.rotation = Quaternion.identity;
        }
        else
            lower.rotation = RotationX(p2 - p1);
    }


    void IK()
    {
        Vector2 p0 = transform.localPosition;
        Vector2 p1 = lower.localPosition;
        Vector2 p2 = hand.localPosition;

        float l1 = Vector2.Distance(p0, p1);
        float l2 = Vector2.Distance(p1, p2);

        // forwards
        p2 = (Vector2)(target.position - transform.position);
        p1 = p2 + (p1 - p2).normalized * l2;

        // backwards
        p1 = p0 + (p1 - p0).normalized * l1;
        p2 = p1 + (p2 - p1).normalized * l2;

        // constrain angle
        float theta = Mathf.Deg2Rad * Vector2.SignedAngle(p1 - p0, p2 - p1);
        if (theta < 0)
        {
            p2 = p1 + (p1 - p0).normalized * l2;
        }


        lower.localPosition = p1;
        hand.localPosition = p2;

        transform.rotation = RotationX(p1 - p0);
        lower.rotation = RotationX(p2 - p1);
    }


    Quaternion RotationX(Vector2 v)
    {
        float theta = Mathf.Atan2(-v.x, v.y);
        return new Quaternion(Mathf.Cos(theta / 2), Mathf.Sin(theta / 2), 0, 0);
    }
    Quaternion RotationY(Vector2 v)
    {
        float theta = Mathf.Atan2(-v.x, v.y);
        return new Quaternion(Mathf.Cos(theta / 2), 0, Mathf.Sin(theta / 2), 0);
    }
}

[CustomEditor(typeof(ProcArms))]
public class ProcArmsEditor : Editor
{
    ProcArms arm;

    private void OnEnable()
    {
        arm = (ProcArms)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //if (GUILayout.Button("Update shader"))
        arm.UpdateJoints();
    }
}