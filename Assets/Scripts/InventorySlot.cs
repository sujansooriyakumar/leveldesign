using System;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDragHandler, IDropHandler
{
    public InventorySlotData _slotData;
    public TMP_Text idDisplay;
    public TMP_Text qtyDisplay;
    public event Action<InventorySlot> OnDragItem;
    public event Action<InventorySlot> OnDropItem;
    public void OnDrag(PointerEventData eventData)
    {
        OnDragItem?.Invoke(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnDropItem?.Invoke(this);
    }

    public InventorySlotData GetSlotData() { return _slotData; }
}

[Serializable]
public struct InventorySlotData
{
    public int itemID;
    public int qty;
    // item data goes here
}