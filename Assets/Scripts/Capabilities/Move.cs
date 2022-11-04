using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Move : MonoBehaviour
{
    public Controller controller;

    public float moveSpeed = 0.2f;

    const float cos45 = 0.7071f;

    public void FixedUpdate()
    {
        Vector2 move = moveSpeed * controller.GetMoveInput();
        transform.position += new Vector3(move.x, move.y * cos45, move.y * cos45);
    }
}
