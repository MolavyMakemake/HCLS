using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public Look look;
    public Controller controller;

    public LineRenderer debug;


    bool _isADS;

    // Update is called once per frame
    void Update()
    {
        _isADS = controller.GetAimInput();
    }

    private void FixedUpdate()
    {
        debug.SetPosition(0, transform.position);

        Vector3 forward = _isADS ? (look.target - transform.position) : 5 * look.normal;

        debug.SetPosition(1, transform.position + forward);
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(forward.y, forward.x));
    }
}
