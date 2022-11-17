using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Grenade : Item
{
    public abstract void OnHit(Collider collider);
}
