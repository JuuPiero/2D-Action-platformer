
using UnityEngine;

[CreateAssetMenu(fileName = "newSkeletonData", menuName = "Data/Enemy/Skeleton")]
public class SkeletonDataSO : EnemyDataSO {
    public float chaseSpeed = 3f;
    public float attackRadius= 1f;
    public float attackCooldownTime = 2f;
}