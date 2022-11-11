using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Transform itemGridUI;
    public bool isOpen;

    public List<ItemGrid> itemGrids = new List<ItemGrid>();

    Canvas _canvas;

    public RectTransform grabbedUI;
    public Vector2 grabbOffset;
    public bool isGrabbed = false;
    public int grabbedFrom;
    public ItemGrid.Slot grabbed;


    private void Update()
    {
        if (!isOpen)
            return;


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 mousePos = Input.mousePosition;

            // Try to grab item
            ItemGrid.Slot _grabbed;
            for (int i = 0; i < itemGrids.Count; i++)
            {
                ItemGrid grid = itemGrids[i];
                if (grid.Grab(mousePos, out _grabbed, itemGridUI.GetChild(grabbedFrom)))
                {
                    grabbed = _grabbed;
                    isGrabbed = true;
                    grabbedFrom = i;

                    Vector2 slotPos = grid.position + new Vector2(grabbed.i % grid.width, -grabbed.i / grid.width) * 50;
                    grabbOffset = mousePos - slotPos - new Vector2(Screen.width, Screen.height) / 2;

                    grabbedUI.gameObject.SetActive(true);
                    grabbedUI.GetComponent<Image>().sprite = grabbed.item.sprite;
                    grabbedUI.GetComponent<RectTransform>().sizeDelta = grabbed.UISize();
                    grabbedUI.transform.rotation = grabbed.quaternion;
                    break;

                }
            }
        }

        if (isGrabbed)
        {
            Vector2 grabbedPos = (Vector2)Input.mousePosition - grabbOffset;

            // If grabbed item is still held
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Vector2 slotOffset = new Vector2(grabbed.width - 1, grabbed.height - 1) * 25;
                grabbedUI.anchoredPosition = grabbedPos + (grabbed.rotated ? -slotOffset : slotOffset);

                if (Input.GetKeyDown(KeyCode.R))
                {
                    grabbed.rotated = !grabbed.rotated;
                    grabbOffset = new Vector2(grabbOffset.y, -grabbOffset.x) * (grabbed.rotated ? 1 : -1);
                    grabbedUI.transform.rotation = grabbed.quaternion;
                }
            }

            // If grabbed item is released
            else
            {
                grabbedUI.transform.rotation = Quaternion.identity;
                grabbedUI.gameObject.SetActive(false);
                isGrabbed = false;

                bool released = false;
                for (int i = 0; i < itemGrids.Count; i++)
                    if (itemGrids[i].Release(grabbedPos, grabbed, itemGridUI.GetChild(grabbedFrom)))
                    {
                        released = true;
                        break;
                    }

                if (!released)
                    itemGrids[grabbedFrom].Add(grabbed, itemGridUI.GetChild(grabbedFrom));
            }
        }
    }

    public void CreateUI()
    {
        for (int i = 0; i < itemGrids.Count; i++)
        {
            var panel = itemGrids[i].CreateUI();
            var rect = panel.GetComponent<RectTransform>();

            panel.transform.SetParent(itemGridUI);
            rect.anchoredPosition = itemGrids[i].position;
        }
    }

    private void OnEnable()
    {
        _canvas = GetComponent<Canvas>();
    }

    public void Open(Player player)
    {
        if (isOpen)
            return;

        _canvas.enabled = true;
        isOpen = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Close()
    {
        _canvas.enabled = false;
        isOpen = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Initialize(Player player)
    {
        if (isOpen)
            Open(player);
        else
            Close();

        itemGrids.Add(player.backpack.itemGrid);
        CreateUI();
    }
}
