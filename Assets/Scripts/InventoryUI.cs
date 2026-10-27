using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public SlotManager slotManager;
    private void Awake()
    {
        slotManager.inventoryUpdated += RefreshUI;
    }
    public void RefreshUI(List<InventorySlot> inventorySlots)
    {
        Debug.Log("refreshing ui");
        foreach (InventorySlot slot in inventorySlots)
        {
            slot.idDisplay.text = slot._slotData.itemID.ToString();
            slot.qtyDisplay.text = slot._slotData.qty.ToString();
        }
    }
}
