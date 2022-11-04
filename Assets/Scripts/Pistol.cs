using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    public override bool Fire()
    {
        Debug.Log(transform.right);

        return true;
    }
}
