using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    public UI ui;
    public Crosshair crosshair;

    public Sprite3D sprite;

    public LineRenderer debug;

    public Vector3 target;
    public Vector3 normal;

    public float theta;

    void OnHoveringObject(Transform hit)
    {
        Loot loot = hit.transform != null ? hit.transform.GetComponent<Loot>() : null;
        if (loot != null)
            ui.DisplayToolTip(loot);
    }

    void ReadInput()
    {
        // This vector shouldnt change and can be changed to a const
        Vector3 cNormal = Camera.main.transform.forward;

        Vector3 position = crosshair == null ? Input.mousePosition : crosshair.position;

        target = Camera.main.ScreenToWorldPoint(position + cNormal);

        RaycastHit hit;
        if (Physics.Raycast(target, cNormal, out hit))
        {
            target = hit.point;
            OnHoveringObject(hit.transform);
        }
        else
            target += (target.y - 1) / -cNormal.y * cNormal;
    }

    private void Update()
    {
        ReadInput();

        Vector3 position = transform.position;
        position.y = target.y;

        normal = (target - position).normalized;

        if (debug != null)
        {
            debug.SetPosition(0, position);
            debug.SetPosition(1, target);
        }

        theta = Mathf.Atan2(normal.z, normal.x);
        if (sprite != null)
            sprite.rotY = Mathf.Rad2Deg * theta;
    }
}
