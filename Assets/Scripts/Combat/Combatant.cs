using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combatant : MonoBehaviour
{
    public float health = 1;
    public float hbRadius = 1;

    public static List<Combatant> list = new List<Combatant>();

    public void Damage(float dmg)
    {
        health -= dmg;
    }

    protected void OnEnable()
    {
        list.Add(this);
    }
}
