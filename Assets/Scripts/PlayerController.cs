using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Controllers/PlayerController")]
public class PlayerController : Controller
{
    public override Vector3 GetMoveInput()
    {
        return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    public override bool GetSecondaryInput()
    {
        return Input.GetKey(KeyCode.Mouse1);
    }

    public override bool GetPrimaryInput()
    {
        return Input.GetKey(KeyCode.Mouse0);
    }
}
