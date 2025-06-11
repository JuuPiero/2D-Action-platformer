using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : MonoBehaviour
{
    public Dictionary<EquipmentSlot, EquipmentDataSO> equippedItems = new();

    [SerializeField] private Player _player;

    public event Action OnEquippedItemsChanged;

    void Awake()
    {
        _player = GetComponentInParent<Player>();
    }

    public void Equip(EquipmentDataSO equipment)
    {
        //REPLACE ITEM
        if (equippedItems.ContainsKey(equipment.slot))
        {
            var item = equippedItems[equipment.slot];
            _player.Inventory.AddItem(item, 1);
        }

        equippedItems[equipment.slot] = equipment;

        Debug.Log($"equipped {equipment.itemName} in {equipment.slot}");
        Debug.Log(equippedItems);
        OnEquippedItemsChanged?.Invoke();
    }

    public void Remove(EquipmentSlot slot)
    {
        equippedItems.Remove(slot);
        // equippedItems[equipment.slot] = equipment;
        // Debug.Log($"equipped {equipment.itemName} in {equipment.slot}");
        // Debug.Log(equippedItems);
        OnEquippedItemsChanged?.Invoke();
    }

}