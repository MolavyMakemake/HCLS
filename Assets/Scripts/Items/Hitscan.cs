using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Gun/Pistol")]
public class Hitscan : Gun
{
    public override bool Fire(Vector3 origin, Vector3 direction)
    {
        Vector2 from = new Vector2(origin.x, origin.z);
        Vector2 n = new Vector2(direction.x, direction.z).normalized;

        foreach (var combatant in Combatant.list)
        {
            Vector2 to = new Vector2(combatant.transform.position.x, combatant.transform.position.z);
            
            // Distance from ray to combatant
            float r = (from + n * Vector2.Dot(to - from, n) - to).magnitude;

            if (r > 0 && r < combatant.hbRadius)
                combatant.Damage(23);
        }

        return true;
    }
}
