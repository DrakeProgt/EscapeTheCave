using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Inventory : MonoBehaviour
{
    //public GameObject hotbarSlots;
    private Slot[] slots;
    private int currentSlotIndex;
    private int prismIndex;

    // Use this for initialization
    private void Start()
    {
        slots = new Slot[9];
        InitSlots();
        currentSlotIndex = 0;
        prismIndex = 1;
        HighlightCurrentSlot(true);
    }

    private void InitSlots()
    {
        for (int i = 0; i < 9; i++)
        {
            slots[i] = new Slot(GameObject.Find("Slot" + (i + 1).ToString()));
            Debug.Log(slots[i]);
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float inventoryAxis = CrossPlatformInputManager.GetAxis("InventoryMove");
        if (inventoryAxis == -1)
            MoveInventoryLeft();
        else if (inventoryAxis == 1)
            MoveInventoryRight();
    }

    private void HighlightCurrentSlot(bool highlight)
    {
        if (highlight)
            slots[currentSlotIndex].SlotUI.GetComponent<Outline>().effectColor = new Color(0, 1, 1, 1);
        else
            slots[currentSlotIndex].SlotUI.GetComponent<Outline>().effectColor = new Color(0, 0, 0, 0);
    }

    private void MoveInventoryRight()
    {
        if (currentSlotIndex <= 7)
        {
            HighlightCurrentSlot(false);
            currentSlotIndex++;

            HighlightCurrentSlot(true);
        }
    }

    private void MoveInventoryLeft()
    {
        if (currentSlotIndex >= 1)
        {
            HighlightCurrentSlot(false);
            currentSlotIndex--;
        }

        HighlightCurrentSlot(true);
    }

    public void AddItemToInventory(GameObject item, ItemType itemType)
    {
        int slotIndex = 0;
        while (!slots[slotIndex].Empty) {
            slotIndex++;
            if(slotIndex > 7)
            {
                return;
            }
        }

        slots[slotIndex].SavedGameObject = item;
        Texture texture = GetTexture(itemType);
        slots[slotIndex].SlotUI.GetComponent<RawImage>().texture = texture;

        slots[slotIndex].Empty = false;
    }

    public GameObject GetSelectedItem()
    {
        return slots[currentSlotIndex].SavedGameObject;
    }

    private Texture GetTexture(ItemType itemType)
    {
        string name = itemType.ToString();
        if (itemType == ItemType.Prism)
        {
            name += prismIndex.ToString();
            prismIndex++;
        }
        return (Texture)Resources.Load(@"Inventory\" + name);
    }

    public GameObject RemoveAndGetSelectedItemFromInventory()
    {
        if (slots[currentSlotIndex].Empty)
            return null;

        slots[currentSlotIndex].Empty = true;
        slots[currentSlotIndex].SlotUI.GetComponent<RawImage>().texture = null;
        return slots[currentSlotIndex].SavedGameObject;
    }
}

public enum ItemType
{
    Lantern = 0,
    Diary = 1,
    Crystal = 2,
    Prism = 3
}

internal class Slot
{
    public Slot(GameObject slotUI)
    {
        Empty = true;
        SavedGameObject = null;
        SlotUI = slotUI;
    }

    public bool Empty { get; set; }

    public GameObject SavedGameObject { get; set; }

    public GameObject SlotUI { get; set; }
}