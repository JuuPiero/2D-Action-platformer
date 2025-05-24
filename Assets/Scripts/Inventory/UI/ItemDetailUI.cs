using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailUI : MonoBehaviour
{
    public TextMeshProUGUI textName;
    public TextMeshProUGUI descriptionText;
    public Button useButton;
    public Button dropButton;

    [SerializeField] private Player _player;
    [SerializeField] private SlotUI _slotUI;


    void Start()
    {
        useButton.onClick.AddListener(() =>
        {
            UseItem();
        });
        dropButton.onClick.AddListener(() =>
        {
            ItemManager.Instance.SpawnItem(_slotUI.itemData, _slotUI.quantity, _player.transform.position + new Vector3(2f, 5f));
            _player.Inventory.RemoveItem(_slotUI.index);
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
    public void ClearDisplay()
    {
        textName.text = "";
        descriptionText.text = "";
        useButton.gameObject.SetActive(false);
        dropButton.gameObject.SetActive(false);
    }

    public void UseItem()
    {
        _slotUI.itemData.Use(_player);

        switch (_slotUI.itemData.itemType)
        {
            case ItemDataSO.ItemType.Equipment:

                break;
            case ItemDataSO.ItemType.Consumable:
                _slotUI.SetQuantity(--_slotUI.quantity);
                if (_slotUI.quantity <= 0) _player.Inventory.RemoveItem(_slotUI.index);
                break;
        }
    }
}