using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Combatant
{
    public Backpack backpack;
    public Inventory inventory;
    public Gun primary;
    public Gun secondary;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inventory.isOpen)
                inventory.Close();
            else
                inventory.Open(this);
        }
    }

    private void OnEnable()
    {
        base.OnEnable();

        inventory.Initialize(this);
    }
}
