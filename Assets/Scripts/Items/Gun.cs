using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Gun : Item
{
    public float fireRate = 0.1f;

    public AnimationCurve recoilX;
    public AnimationCurve recoilY;

    public abstract bool Fire(Vector3 origin, Vector3 direction);
}
