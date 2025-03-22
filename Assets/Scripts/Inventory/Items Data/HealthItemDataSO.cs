using UnityEngine;

[CreateAssetMenu(fileName = "newHealthItemData", menuName = "Data/Item Data/Health")]
public class HealthItemDataSO : ItemDataSO 
{
    public float healthRestore;

    public override void Use()
    {
        Debug.Log("Player healing " + this.healthRestore + "hp");
    }
}