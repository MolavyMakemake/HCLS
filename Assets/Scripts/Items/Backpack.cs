using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Item/Backpack")]
public class Backpack : Item
{
    public int foldedWidth;
    public int foldedHeight;

    public bool folded;

    public ItemGrid itemGrid;


    public void Fold()
    {

    }

    public void UnFold()
    {
        
    }

    public void Open()
    {

    }

    public bool AddItem(int i)
    {
        return false;
    }

    public void RemoveItem(int i)
    {

    }
}
