using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public Canvas canvas;

    RectTransform _rect;

    public float lastHover;
    public bool visible;

    public Loot hovering;

    public float duration;

    public void Display(Loot loot)
    {
        hovering = loot;
        visible = true;
        canvas.enabled = true;

        var content = GetComponentsInChildren<TMPro.TMP_Text>();
        content[0].text = loot.item.id;
        content[1].text = loot.item.description;

        CenterToolTip();
    }

    public void Hide()
    {
        visible = false;
        canvas.enabled = false;
    }

    private void Update()
    {
        if (visible)
        {
            if (Time.time - lastHover > duration)
                Hide();

            else
                CenterToolTip();
        }
    }

    void CenterToolTip()
    {
        Vector2 mousePos = Camera.main.WorldToScreenPoint(hovering.transform.position);
        _rect.anchoredPosition = mousePos;
    }

    private void OnEnable()
    {
        _rect = GetComponent<RectTransform>();
    }
}
