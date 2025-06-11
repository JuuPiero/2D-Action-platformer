using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    [SerializeField] public Image _icon;
    [SerializeField] public Image _border;
    [SerializeField] protected Button _button;
    [SerializeField] protected TextMeshProUGUI _quantityText;

    private Inventory.Slot _slot;
    public ItemDataSO itemData;
    public int quantity;
    public int index;
    public InventoryUI inventoryUI;

   

    void Start()
    {
        _button.onClick.AddListener(() =>
        {
            inventoryUI.GetSlotsUI().ForEach(slotUI =>
            {
                slotUI.SetSelected(false);
            });
            SetSelected(true);
            inventoryUI.itemDetailUI.Display(this);
        });
    }




    public void SetIndex(int index)
    {
        this.index = index;
    }

    public void SetData(ItemDataSO data)
    {
        itemData = data;
        _icon.sprite = itemData.icon;
        _icon.gameObject.SetActive(true);
    }

    public void SetQuantity(int quantity)
    {
        this.quantity = quantity;
        _quantityText.text = quantity.ToString();
    }

    public void SetSlot(Inventory.Slot slot)
    {
        inventoryUI = GetComponentInParent<InventoryUI>();
        _slot = slot;
        quantity = slot.quantity;
        itemData = _slot.itemData;
        _icon.sprite = _slot.itemData.icon;
        _icon.gameObject.SetActive(true);
        _quantityText.text = _slot.quantity.ToString();
    }

    public void SetSelected(bool selected)
    {
        if (selected)
        {
            _border.color = Color.green;
        }
        else
        {
            _border.color = Color.white;
        }
    }

    
}