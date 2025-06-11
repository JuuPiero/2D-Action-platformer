using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public event Action OnInventoryChanged;
    public List<Slot> slots = new();

    void Awake()
    {
        OnInventoryChanged += SortByName;
    }

    [Serializable]
    public class Slot
    {
        public const int MAX_ITEM = 99;
        public ItemDataSO itemData;
        public int quantity;


        public bool CanAddToSlot(Item item)
        {
            return CanAddToSlot(item.data);
        }
        public bool CanAddToSlot(ItemDataSO _itemData)
        {
            return itemData.itemType == _itemData.itemType && itemData.isStackable && itemData.itemName == _itemData.itemName;
        }
    }

    public void AddItem(Item item)
    {
        AddItem(item.data, item.quantity);
    }

    public void AddItem(ItemDataSO itemData, int quantity)
    {
        foreach (Slot slot in slots)
        {
            if (slot.CanAddToSlot(itemData))
            {
                if (quantity + slot.quantity > Slot.MAX_ITEM)
                {
                    slot.quantity = Slot.MAX_ITEM;
                    int temp = (quantity + slot.quantity) - Slot.MAX_ITEM;
                    AddNewSlot(itemData, temp);
                }
                else
                {
                    slot.quantity += quantity;
                }
                OnInventoryChanged?.Invoke();
                return;
            }
        }
        AddNewSlot(itemData, quantity);
        OnInventoryChanged?.Invoke();
    }

    private void AddNewSlot(ItemDataSO data, int quantity)
    {
        slots.Add(new Slot
        {
            itemData = data,
            quantity = quantity
        });
    }

    public Slot GetSlot(int slotIndex)
    {
        return slots[slotIndex];
    }

    public void RemoveItem(int slotIndex)
    {
        slots.RemoveAt(slotIndex);
        OnInventoryChanged?.Invoke();
    }

    private void SortByName()
    {
        var sorted = slots
            .OrderBy(i => i.itemData.itemType)
            .ThenBy(i => i.itemData.itemName)
            .ThenByDescending(i => i.quantity)
            .ToList();
        slots = sorted;
    }
    void OnDisable()
    {
        OnInventoryChanged -= SortByName;
    }
}