using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprite3D : MonoBehaviour
{
    public string sprite;

    public float minRot = 0;
    public float maxRot = 360;

    public float rotY;
    public float rotZ;

    float dTheta;

    Sprite[] spriteSheet;
 
    SpriteRenderer spriteRenderer;

    private void Update()
    {
        dTheta = (maxRot - minRot) / spriteSheet.Length;

        float theta = rotY + dTheta / 2 + 90;

        if (theta < 0)
            theta += 360;


        int i = (int)Mathf.Clamp(theta / dTheta, 0, spriteSheet.Length - 1);
        Debug.Log(theta + ", " + i);
        spriteRenderer.sprite = spriteSheet[i];
    }

    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteSheet = Resources.LoadAll<Sprite>(sprite);
        if (spriteSheet.Length == 0)
        {
            Debug.Log("Could not find Assets/Resources/" + sprite);
            Destroy(this); return;
        }    
    }
}
