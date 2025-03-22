
using UnityEngine;

[CreateAssetMenu(fileName = "newSlimeData", menuName = "Data/Enemy/Slime")]
public class SlimeDataSO : EnemyDataSO 
{
    public Vector2 jumpForce = new Vector2(2f, 5f);
    public float groundCheckRadius = 0.2f;
    public float jumpCoolDownTime = 1f;

    [Header("Attack")]
    public float chaseRadius = 1f;
    public float attackRadius = 0.3f;
    public float damageRadius = 0.6f;
    public int damage = 10;

    public float attackCoolDownTime = 1f;
    
    public Vector2 hitForce = new Vector2(10f, 4f);
    public Vector2 attackForce = new Vector2(10f, 4f);

}
