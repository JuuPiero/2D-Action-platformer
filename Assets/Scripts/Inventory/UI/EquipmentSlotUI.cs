using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlotUI : MonoBehaviour
{
    public Image icon;
    [SerializeField] private Image _border;

    public Button button;
    public TextMeshProUGUI textName;
    public EquipmentDataSO itemData;

    public EquipmentUI equipmentUI;

    public InventoryUI inventoryUI;

    void Awake()
    {
        equipmentUI = GetComponentInParent<EquipmentUI>();
        inventoryUI = GetComponentInParent<InventoryUI>();
    }

    void Start()
    {
        button.onClick.AddListener(() =>
        {
            equipmentUI.slots.ForEach(slotUI =>
            {
                slotUI.SetSelected(false);
            });
            SetSelected(true);
            inventoryUI?.itemDetailUI.Display(this);
        });
    }

    public void SetData(ItemDataSO data)
    {
        itemData = (EquipmentDataSO)data;
        icon.sprite = itemData.icon;
        textName.text = itemData.slot.ToString();
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