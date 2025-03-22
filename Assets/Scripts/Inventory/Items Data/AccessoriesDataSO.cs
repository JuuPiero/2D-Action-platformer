using UnityEngine;

[CreateAssetMenu(fileName = "newAccessoriesData", menuName = "Data/Item Data/Accessories")]
public class AccessoriesDataSO : ItemDataSO 
{
    public float buffAtk;

    public override void Use()
    {
        Debug.Log("Player buff " + this.buffAtk + "atk");
    }
}