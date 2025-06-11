using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Items/Consumable")]
public class ConsumableDataSO : ItemDataSO
{
    public float healAmount;
    public float manaAmount = 0f;

    public override void Use(object player)
    {
        
        if (player is Player _player)
        {
            var resource = _player.Resource;

            resource.CurrentHealth = Mathf.Min(resource.CurrentHealth + healAmount, _player.Data.maxHealthPoint);
            resource.CurrentMana = Mathf.Min(resource.CurrentMana + manaAmount, _player.Data.maxMana);
        }
    }
}