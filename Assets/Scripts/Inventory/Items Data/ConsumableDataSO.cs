using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Items/Consumable")]
public class ConsumableDataSO : ItemDataSO
{
    public int healAmount;

    public override void Use(Player player)
    {
    }
}