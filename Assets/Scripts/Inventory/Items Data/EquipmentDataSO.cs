using UnityEngine;

[CreateAssetMenu(fileName = "newEquipmentData", menuName = "Items/Equipment item")]
public class EquipmentDataSO : ItemDataSO
{
    public EquipmentSlot slot; // Vị trí trang bị
    public int defenseBonus;
    public int attackBonus;

    public override void Use(Player player)
    {
        player.Equip(this);
    }
}
