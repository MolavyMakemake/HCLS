using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public Tooltip tooltip;
    public Loot hovering { get => tooltip.visible ? tooltip.hovering : null; }

    public void DisplayToolTip(Loot loot)
    {
        tooltip.lastHover = Time.time;
    
        if (loot != hovering)
            tooltip.Display(loot);
    }
}
