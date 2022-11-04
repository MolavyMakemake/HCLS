using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Controllers/PlayerController")]
public class PlayerController : Controller
{
    public override Vector3 GetMoveInput()
    {
        return new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    public override Vector3 GetLookInput()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    public override bool GetAimInput()
    {
        return Input.GetKey(KeyCode.Mouse1);
    }

    public override bool GetFireInput()
    {
        return Input.GetKey(KeyCode.Mouse0);
    }
}
