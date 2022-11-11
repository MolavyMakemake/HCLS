using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    public Vector2 position;

    public float spread = 1;

    [Header("Visuals")]

    public Vector2 shape;
    public float minSpread;
    public float maxSpread;

    RectTransform _rect;

    private void Update()
    {
        UpdatePosition();

        _rect.anchoredPosition = position; 
    }

    void UpdatePosition()
    {
        position.x += Input.GetAxis("Mouse X");
        position.y += Input.GetAxis("Mouse Y");
    }

    void UpdateVisuals()
    {
        RectTransform[] rect = GetComponentsInChildren<RectTransform>();

        Vector2 s = 50 * shape;

        rect[1].sizeDelta = rect[3].sizeDelta = s;
        rect[2].sizeDelta = rect[4].sizeDelta = new Vector2(s.y, s.x);

        float x = s.x / 2;
        rect[1].anchoredPosition = new Vector2(x, 0);
        rect[2].anchoredPosition = new Vector2(0, x);
        rect[3].anchoredPosition = new Vector2(-x, 0);
        rect[4].anchoredPosition = new Vector2(0, -x);

        SetSpreadIndicator(spread);

    }

    public void SetSpreadIndicator(float spread)
    {
        this.spread = spread;
        float x = 100 * Mathf.Clamp(spread, minSpread, maxSpread);
        _rect.sizeDelta = new Vector2(x, x);
    }


    private void OnEnable()
    {
        position = Input.mousePosition;
        _rect = GetComponent<RectTransform>();
        UpdateVisuals();
    }
}
