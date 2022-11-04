using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : ScriptableObject
{
    public abstract Vector3 GetMoveInput();
    public abstract Vector3 GetLookInput();

    public abstract bool GetAimInput();
    public abstract bool GetFireInput();
}
