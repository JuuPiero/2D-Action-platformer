

using System;
using System.Collections.Generic;

[System.Serializable]
public class Inventory 
{
    public event Action OnInventoryChanged;
    public List<Slot> slots = new List<Slot>(); 

    [System.Serializable]
    public class Slot 
    {
        public const int MAX_ITEM = 99;
        public ItemDataSO itemData;
        public int quantity;


        public bool CanAddToSlot(Item item) {
            return quantity + item.quantity < MAX_ITEM && itemData.itemType == item.data.itemType && itemData.isStackable && itemData.itemName == item.data.itemName;
        }
    }

    public void AddItem(Item item) 
    {
        foreach (Slot slot in slots) 
        {
            if(slot.CanAddToSlot(item)) 
            {
                slot.quantity += item.quantity;
                OnInventoryChanged?.Invoke();
                return;
            }
        }

        slots.Add(new Slot {
            itemData = item.data,
            quantity = item.quantity
        });
        OnInventoryChanged?.Invoke();
    }


    public Slot GetSlot(int slotIndex) {
        return slots[slotIndex];
    }
}