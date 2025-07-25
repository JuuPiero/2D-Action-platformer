using UnityEngine;

[CreateAssetMenu(fileName = "newEquipmentData", menuName = "Items/Equipment item")]
public class EquipmentDataSO : ItemDataSO
{
    public EquipmentSlot slot; // Vị trí trang bị
    public int defenseBonus;
    public int attackBonus;

    public override void Use(object player)
    {
        if (player is Player _player)
            _player.EquipmentManager?.Equip(this);
    }
    
    public void Equip(GameObject player)
    {
        // if(player is Player _player)
        //     _player.Equip(this);
    }
}
