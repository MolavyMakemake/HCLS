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

    public void Throw(Vector3 origin, Vector3 velocity)
    {
        var go = new GameObject("thrown " + id, typeof(SpriteRenderer), typeof(Rigidbody), typeof(SphereCollider));
        go.GetComponent<SpriteRenderer>().sprite = sprite;

        go.transform.position = origin + Vector3.up;
        go.GetComponent<Rigidbody>().velocity = velocity;
    }
}
