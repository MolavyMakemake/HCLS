using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : Combatant
{
    public enum State : int
    {
        Passive,
        Hostile,
        Searching,
        Scared
    }

    public State state = State.Passive;
}
