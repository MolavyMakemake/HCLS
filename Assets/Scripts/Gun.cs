using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public float fireRate = 0.1f;
    public float recoil = 0.1f;

    public abstract bool Fire();
}
