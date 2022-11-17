using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    public Item item;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            item.Throw(transform.position, transform.forward);
    }
}
