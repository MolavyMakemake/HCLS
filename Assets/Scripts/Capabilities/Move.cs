using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Move : MonoBehaviour
{
    public Controller controller;

    public float moveSpeed = 0.2f;

    public void FixedUpdate()
    {
        Vector3 move = moveSpeed * controller.GetMoveInput();
        transform.position += move;
    }
}
