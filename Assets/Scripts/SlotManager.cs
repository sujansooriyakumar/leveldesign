using System;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public List<InventorySlot> inventorySlots;
    public InventorySlotData itemDragged;
    public InventorySlot prevSelected;
    public event Action<List<InventorySlot>> inventoryUpdated;
    private void Awake()
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            slot.OnDragItem += HandleDragItem;
            slot.OnDropItem += HandleDropItem;
        }
    }

    private void Start()
    {
        inventoryUpdated?.Invoke(inventorySlots);

    }

    private void HandleDropItem(InventorySlot slot)
    {
        Debug.Log(slot.ToString());
        if(slot._slotData.itemID > 0)
        {
            prevSelected._slotData = slot._slotData;
            slot._slotData = itemDragged;
        }
        itemDragged = default;
        inventoryUpdated?.Invoke(inventorySlots);
        
        // use slot reference to replace current slot data 
    }

    private void HandleDragItem(InventorySlot slot)
    {
        Debug.Log(slot.ToString());
        prevSelected = slot;
        itemDragged = slot._slotData;
        // use slot reference to empty item
        // store reference to currently dragged item maybe with slot.GetSlotData
    }
}
