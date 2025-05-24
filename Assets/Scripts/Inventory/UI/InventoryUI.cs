using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject _root;

    public GameObject slotUIPrefab;
    public GameObject itemListPanel;
    public GameObject itemDetailPanel;
    public GameObject equipmentPanel;

    [SerializeField] protected Player _player;

    // public List<SlotUI> slotUIs;

    public void Toggle()
    {
        _root.SetActive(!_root.activeSelf);
        _player.InputHandler.SetGameplay(!_root.activeSelf);
    }

    void Start()
    {
        Load();
        _player.Inventory.OnInventoryChanged += Load;
    }

    public void Load()
    {
        itemListPanel.transform.ClearChild();
        var slots = _player.Inventory.slots;

        slots.ForEach(slot =>
        {
            int index = slots.IndexOf(slot);
            GameObject slotGO = Instantiate(slotUIPrefab);
            slotGO.transform.SetParent(itemListPanel.transform, false);
            SlotUI slotUI = slotGO.GetComponent<SlotUI>();
            slotUI.SetSlot(slot);
            slotUI.SetIndex(index);
            slotUI.inventoryUI = this;
         
        });
    }

    public List<SlotUI> GetSlotsUI()
    {
        return itemListPanel.transform.GetChilds<SlotUI>();
    }
    public SlotUI GetSlot(int index)
    {
        return GetSlotsUI()[index];
    }

    public void DisplayDetail(SlotUI slot)
    {
        itemDetailPanel.GetComponent<ItemDetailUI>().Display(slot);
    }

}