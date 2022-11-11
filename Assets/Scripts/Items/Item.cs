using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Item : ScriptableObject
{
    public Sprite sprite;
    public string id;
    [TextArea(3, 5)]public string description;
    public int width;
    public int height;
}
