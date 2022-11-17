using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Item/Grenade/Frag Grenade")]
public class FragGrenade : Grenade
{
    public override void OnHit(Collider collider)
    {
        throw new System.NotImplementedException();
    }
}
