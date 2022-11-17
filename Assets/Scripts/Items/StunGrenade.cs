using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Item/Grenade/Stun Grenade")]
public class StunGrenade : Grenade
{
    public override void OnHit(Collider collider)
    {
        throw new System.NotImplementedException();
    }
}
