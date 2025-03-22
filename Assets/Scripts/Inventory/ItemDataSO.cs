using UnityEngine;

public abstract class ItemDataSO : ScriptableObject {
    public ItemType itemType;
    public string itemName;
    public Sprite icon;
    public bool isStackable;

    [TextArea] public string description;

    public abstract void Use();

    public enum ItemType 
    {
        None,
        Accessories,
        Armor,
        Consumable,
        // QuestItem,
        // CraftingMaterial,
    }
}