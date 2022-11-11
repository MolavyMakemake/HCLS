using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : Controller
{
    Animal animal;

    public override Vector3 GetMoveInput()
    {
        return animal.state switch
        {
            Animal.State.Passive => new Vector3(),
            Animal.State.Searching => new Vector3(),
            Animal.State.Hostile => new Vector3(),
            Animal.State.Scared => new Vector3(),

            _ => new Vector3()
        };
    }

    public override bool GetSecondaryInput()
    {
        throw new System.NotImplementedException();
    }

    public override bool GetPrimaryInput()
    {
        throw new System.NotImplementedException();
    }
}
