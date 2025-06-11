using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailUI : MonoBehaviour
{
    public TextMeshProUGUI textName;
    public TextMeshProUGUI descriptionText;
    public Button useButton;
    public TextMeshProUGUI useTextButton;
    public Button dropButton;


    public InventoryUI inventoryUI;

    [SerializeField] private Player _player;
    [SerializeField] private SlotUI _slotUI;

    void Awake()
    {
        _player = FindFirstObjectByType<Player>();
    }

    void Start()
    {
        inventoryUI = GetComponentInParent<InventoryUI>();
        useButton.onClick.AddListener(() =>
        {
            UseItem();
        });
        dropButton.onClick.AddListener(() =>
        {
            ItemManager.Instance?.SpawnItem(_slotUI.itemData, _slotUI.quantity, _player.transform.position + new Vector3(2f * _player.transform.localScale.x, 5f));
            _player?.Inventory.RemoveItem(_slotUI.index);
        });
    }

    public void Display(SlotUI slot)
    {
        useButton.gameObject.SetActive(true);
        dropButton.gameObject.SetActive(true);

        _slotUI = slot;

        textName.text = _slotUI.itemData.name;
        descriptionText.text = _slotUI.itemData.description;

        var useButtonText = useButton.GetComponentInChildren<TextMeshProUGUI>();

        switch (_slotUI.itemData.itemType)
        {
            case ItemDataSO.ItemType.Equipment:
                useButtonText.text = "Equip";
                break;
            case ItemDataSO.ItemType.Consumable:
                useButtonText.text = "Use";
                break;
        }
    }


    public void Display(ItemDataSO data)
    {
        useButton.gameObject.SetActive(true);
        dropButton.gameObject.SetActive(true);
        textName.text = data.name;
        descriptionText.text = data.description;
    }

    public void Display(EquipmentSlotUI slot)
    {
        Display(slot.itemData);
        useTextButton.text = "Uequip";
    }


    public void SetTextUseButton(string text)
    {
        useTextButton.text = text;
    }
    public void ClearDisplay()
    {
        textName.text = "";
        descriptionText.text = "";
        useButton.gameObject.SetActive(false);
        dropButton.gameObject.SetActive(false);
    }

    public void UseItem()
    {
        Player player = FindFirstObjectByType<Player>();
        _slotUI.itemData.Use(player);

        switch (_slotUI.itemData.itemType)
        {
            case ItemDataSO.ItemType.Equipment:
                _player.Inventory.RemoveItem(_slotUI.index);
                break;
            case ItemDataSO.ItemType.Consumable:
                int quantity = _slotUI.quantity - 1;
                _slotUI.SetQuantity(quantity);
                if (_slotUI.quantity <= 0) _player.Inventory.RemoveItem(_slotUI.index);
                break;
        }
    }
}