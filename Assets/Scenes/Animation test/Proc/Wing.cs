using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wing : ProceduralAnimation
{
    public float flapRate = 0.1f;

    public float t = 0;

    public Transform outer;
    public Transform inner;

    public Transform flapping;

    public enum State : int
    {
        Folded, Display, Flapping
    }


    private void FixedUpdate()
    {
        outer.localRotation = flapping.rotation;
        inner.localRotation = flapping.GetChild(1).localRotation;
    }
}
