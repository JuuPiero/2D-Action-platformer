
using UnityEngine;

[CreateAssetMenu(fileName = "newSkeletonData", menuName = "Data/Enemy/Skeleton")]
public class SkeletonDataSO : EnemyDataSO {
    public float chaseSpeed = 2f;
    public float attackRadius= 1f;
    public float attackCooldownTime = 2f;

    public float knockbackForce = 8f;

}