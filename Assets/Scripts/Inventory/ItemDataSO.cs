using UnityEngine;
public enum EquipmentSlot { Head, Chest, Legs, Weapon, Ring, Accessory }

public abstract class ItemDataSO : ScriptableObject {
    public enum ItemType { Equipment, Consumable }

    public ItemType itemType;
    public string itemName;
    public Sprite icon;
    public bool isStackable;

    [TextArea] public string description;

    public abstract void Use(Player player);

}