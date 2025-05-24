using UnityEngine;
public enum EquipmentSlot { Head, Armor, Legs, Weapon, Ring, Accessory }

public abstract class ItemDataSO : ScriptableObject {
    public enum ItemType { Equipment, Consumable, Coin }

    public ItemType itemType;
    public string itemName;
    public Sprite icon;
    public bool isStackable;

    [TextArea] public string description;

    public abstract void Use(object player);
}