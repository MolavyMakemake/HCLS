using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

[Serializable]
public class ItemGrid
{
    public ItemGrid(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public int width;
    public int height;

    public Vector2 position;

    [Serializable]
    public class Slot
    {
        static Quaternion rotation90deg = Quaternion.Euler(0, 0, -90);

        public Item item;
        public int width => rotated ? item.height : item.width;
        public int height => rotated ? item.width : item.height;
        public int i;
        public bool rotated;
        public Quaternion quaternion => rotated ? rotation90deg : Quaternion.identity;

        public Vector2 UISize() => new Vector2(item.width, item.height) * 50;
    }

    public List<Slot> slots;

    public Slot? SlotFromIndex(int i)
    {
        int x = i % width;
        int y = i / height;

        foreach (var slot in slots)
        {
            // Get lowest/highest item index
            int i0 = slot.i;
            int i1 = slot.i + (slot.width - 1) + (slot.height - 1) * width;

            // Check if x, y is in the rect
            bool inColumn = x >= i0 % width && x <= i1 % width;
            bool inRow = y >= i0 / width && y <= i1 / width;
            if (inColumn && inRow)
                return slot;
        }

        return null;
    }

    public int IndexFromPosition(Vector2 v)
    {
        // set position relative to center
        v -= new Vector2(Screen.width, Screen.height) / 2;
        v = (v - position) / 50;

        // round to nearest int
        int x = (int)(v.x + .5f);
        int y = (int)(.5f - v.y);

        // check if position is valid
        if (x >= 0 && x < width && y >= 0 && y < height)
            return y * width + x;

        else
            return -1;
    }

    // Used for creating ui. Since slots only store index their position needs to be calculated
    public Vector3 PositionFromIndex(int i) => new Vector3(50 * (i % width), -50 * (i / width));

    public bool Grab(Vector2 mousePos, out Slot? slot, Transform UI)
    {
        int i = IndexFromPosition(mousePos);
        if (i != -1)
        {
            slot = SlotFromIndex(i);
            if (slot != null)
                Remove(slot, UI);

            return slot != null;
        }

        slot = null;
        return false;
    }

    public bool Release(Vector2 mousePos, Slot slot, Transform UI)
    {
        int i = IndexFromPosition(mousePos);
        if (i != -1)
        {
            // Should be optimized later lol
            bool canFit = i + (slot.width - 1) + (slot.height - 1) * width < width * height;
            canFit &= (i % width + slot.width - 1) < width;

            for (int y = 0; y < slot.height; y++)
                for (int x = 0; x < slot.width; x++)
                    canFit &= SlotFromIndex(i + y * width + x) == null;

            if (canFit)
            {
                slot.i = i;
                Add(slot, UI);
                return true;
            }
        }
        return false;
    }

    public void Add(Slot slot, Transform UI)
    {
        slots.Add(slot);
        AddSlotUI(UI.GetComponentsInChildren<Transform>().Skip(1).ToArray(), slot);
    }

    public void Remove(Slot slot, Transform UI)
    {
        slots.Remove(slot);
        RemoveSlotUI(UI.GetComponentsInChildren<Transform>().Skip(1).ToArray(), slot);
    }

    public void RemoveSlotUI(Transform[] ui, Slot slot)
    {
        for (int j = 0; j < slot.height; j++)
            for (int i = 0; i < slot.width; i++)
            {
                ui[slot.i + j * width + i].GetComponent<Image>().enabled = true;
            }

        var rect = ui[slot.i].GetComponent<RectTransform>();

        var image = rect.GetComponent<Image>();
        image.sprite = Resources.Load("unity_builtin_extra/Background.png") as Sprite;
        image.color = new Color(1, 1, 1, 0.4f);

        rect.anchoredPosition = PositionFromIndex(slot.i);
        rect.sizeDelta = new Vector2(50, 50);
    }

    public void AddSlotUI(Transform[] ui, Slot slot)
    {
        // Hide all covered slots
        for (int j = 0; j < slot.height; j++)
            for (int i = 0; i < slot.width; i++)
            {
                ui[slot.i + j * width + i].GetComponent<Image>().enabled = false;
            }


        // Set image and resize ui
        var rect = ui[slot.i].GetComponent<RectTransform>();
        var image = rect.GetComponent<Image>();

        image.enabled = true;
        image.sprite = slot.item.sprite;
        image.color = Color.white;

        rect.anchoredPosition += 25 * new Vector2(slot.width - 1, 1 - slot.height);
        rect.sizeDelta = 50 * new Vector2(slot.item.width, slot.item.height);
        rect.rotation = slot.quaternion;
    }

    // Returns gameobjects to be instantiated for the ui. Called via the Player class
    public GameObject CreateUI()
    {
        GameObject panel = new GameObject("grid", typeof(RectTransform));
        Transform[] ui = new Transform[width * height];

        // Create all empty slots
        for (int i = 0; i < ui.Length; i++)
        {
            // Components are added one by one since we are not instantiating a prefab
            ui[i] = new GameObject("Slot " + i, typeof(RectTransform), typeof(Image)).transform;
            ui[i].SetParent(panel.transform);
            var rect = ui[i].GetComponent<RectTransform>();
            var img = ui[i].GetComponent<Image>();

            rect.sizeDelta = new Vector2(50, 50);
            rect.anchoredPosition = PositionFromIndex(i);

            img.sprite = Resources.Load("unity_builtin_extra/Background.png") as Sprite;
            img.color = new Color(1, 1, 1, 0.4f);
        }

        // Replace empty slots with filled ones
        foreach (var slot in slots)
            AddSlotUI(ui, slot);

        return panel;
    }
}
