using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EquipmentUI : MonoBehaviour
{
    public InventoryUI inventoryUI;
    public List<EquipmentSlotUI> slots = new();
    [SerializeField] public Player _player;


    void Awake()
    {
        _player = FindFirstObjectByType<Player>();
        inventoryUI = GetComponentInParent<InventoryUI>();
        slots = transform.GetChildsRecursive<EquipmentSlotUI>();
    }


    void OnEnable()
    {
        _player.EquipmentManager.OnEquippedItemsChanged += Load;
    }

    public void Load()
    {
        // transform.ClearChild();
        var equippedItems = _player.EquipmentManager.equippedItems;
        int index = 0;
        foreach (var item in equippedItems)
        {
            slots[index].SetData(item.Value);
            index++;
        }
    }

    public void Unequip()
    {

    }
    
}