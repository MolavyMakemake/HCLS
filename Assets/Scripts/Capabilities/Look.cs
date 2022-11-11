using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    public UI ui;
    public Crosshair crosshair;

    public LineRenderer debug;

    public Vector3 target;
    public Vector3 normal;

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

        target = Camera.main.ScreenToWorldPoint((Vector3)crosshair.position + cNormal);

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
        position.y = 1;

        normal = (target - position).normalized;

        debug.SetPosition(0, position);
        debug.SetPosition(1, target);

        transform.rotation = Quaternion.Euler(0, -Mathf.Rad2Deg * Mathf.Atan2(normal.z, normal.x), 0);
    }
}
