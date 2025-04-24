using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryUI : MonoBehaviour
{
    public Player _player;
    [SerializeField] bool _active = false;
    [SerializeField] private UIDocument _inventoryDocument;
    [SerializeField] private VisualTreeAsset _slotDocumentTemplate;
    private VisualElement _root;

    [SerializeField] private List<VisualElement> _slots;

    void Awake()
    {
        _inventoryDocument = GetComponent<UIDocument>();
        _player = FindAnyObjectByType<Player>();
        _slots = new List<VisualElement>();
        if(_inventoryDocument != null) {
            _root = GetComponent<UIDocument>().rootVisualElement;
            //_root.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
        }
    }

    void Start() {
        if (_player?.Inventory != null)
        {
            _player.Inventory.OnInventoryChanged += ResetInventory;
            // _player.Inventory.slots.ForEach((slot) => {
            //     VisualElement item = _slotDocumentTemplate.CloneTree();
            //     item.Q<Image>("itemIcon").image = slot.itemData.icon.texture;
            //     item.Q<Label>("itemQuantity").text = "x" + slot.quantity.ToString();
            //     _slots[_player.Inventory.slots.IndexOf(slot)] = item;
            // });
        }
        if(_slotDocumentTemplate == null) {
            Debug.LogWarning("Drag slot template !!!!!!");
        }
        // ResetInventory();
    }

    public void ResetInventory()
    {
        VisualElement inventoryContainer = _root.Query<VisualElement>().Class("list-item").First();
        inventoryContainer.Clear();

        _player.Inventory.slots.ForEach(slot => {
            VisualElement item = _slotDocumentTemplate.CloneTree();
            // item.Query<Label>().Class("list-item").First().text = slot.quantity.ToString();
            item.Q<Image>("itemIcon").image = slot.itemData.icon.texture;
            // item.Q<Label>("itemQuantity").text = "x" + slot.quantity.ToString();
            // item.Query<Label>().Class("item-quantity").First().text = slot.quantity.ToString();
            item.GetElementByClassName<Label>("item-quantity").text = "x" + slot.quantity.ToString();

            inventoryContainer.Add(item);

            item.RegisterCallback<ClickEvent>(e =>
            {
                // var items = _root.Query<VisualElement>().Class("list-item").ToList();
                // items.ForEach(item => {
                //     item.GetElementByName<Label>("item").RemoveFromClassList("selected");
                // });
                item.GetElementByClassName<VisualElement>("item").AddToClassList("selected");
                slot.itemData.Use(_player);
            });
        });

    }

    void GetDetailSlot(Inventory.Slot slot) {
        Debug.Log(slot);
    }

    void OnDestroy()
    {
        if (_player?.Inventory != null)
        {
            _player.Inventory.OnInventoryChanged -= ResetInventory;
        }
    }

    public void ToggleInventory() {
        _root.style.display = _active ? new StyleEnum<DisplayStyle>(DisplayStyle.None) : new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
        _active = !_active;
    }

    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.B)) {
        //     ToggleInventory();
        // }
    }
}