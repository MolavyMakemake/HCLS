using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    public Controller controller;
    public LineRenderer debug;

    public Vector3 target;
    public Vector3 normal;

    private void Update()
    {
        target = controller.GetLookInput();
        normal = (target - transform.position).normalized;

        debug.SetPosition(0, transform.position);
        debug.SetPosition(1, target);
    }

    private void FixedUpdate()
    {
        
    }
}
