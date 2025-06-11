using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject _root;

    public GameObject slotUIPrefab;
    public GameObject itemListPanel;
    public GameObject equipmentPanel;

    public ItemDetailUI itemDetailUI;

    public Image playerAvatar;

    [SerializeField] protected Player _player;
    [SerializeField] protected Inventory _inventory;

   

    public void Toggle()
    {
        _root.Toggle();
        _player.InputHandler.SetGameplayInput(!_root.activeSelf);
    }
    void Awake()
    {
        _player = FindFirstObjectByType<Player>();
        _inventory = _player.Inventory;
        playerAvatar.sprite = _player.GetComponentInChildren<SpriteRenderer>().sprite;
    }
    void Start()
    {
        LoadInventory();
        _player.Inventory.OnInventoryChanged += LoadInventory;
    }
    void OnDisable()
    {
        _player.Inventory.OnInventoryChanged -= LoadInventory;
    }
    
    void Update()
    {
        if (_player.InputHandler.IsInventoryPressed)
        {
            Toggle();
        }
    }

    public void LoadInventory()
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
        itemDetailUI.GetComponent<ItemDetailUI>().Display(slot);
    }

}