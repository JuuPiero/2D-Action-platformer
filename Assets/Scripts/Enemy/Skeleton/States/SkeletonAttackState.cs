
using UnityEngine;

public class SkeletonAttackState : SkeletonState {
    public static int attackIndex = 1;
    const int MaxAttack = 2;

    public string AnimationBaseName = "";
    public SkeletonAttackState(string animationName, Skeleton skeleton) : base(animationName, skeleton)
    {
        AnimationBaseName = animationName;
    }

    public override bool IsMatchingConditions()
    {
        return _skeleton.canAttack && _skeleton.attackTimer.IsReady;
    }

    public override void Enter() {
        _skeleton.GetComponentInChildren<EnemyAnimation>().OnAnimationTrigger += Attack;

        if(attackIndex > MaxAttack) attackIndex = 1;
        AnimationBoolName = AnimationBaseName + "_" + attackIndex;
        base.Enter();
      
    }

    public override void Exit() {
        base.Exit();
        attackIndex++;
    }

    public void Attack() {
        var playerDetect = Physics2D.OverlapCircleAll(_skeleton.attackPoint.position, _skeleton.Data.attackRadius, _skeleton.playerLayer);

        foreach (var player in playerDetect) {
            VFXManager.Instance?.PlayEffect("HitVFX", _skeleton.attackPoint.position, 0.3f);
            player.GetComponent<IDamageable>().Damage(_skeleton.Data.basicAttackDamage);
            player.GetComponent<Player>().Knockback((player.transform.position - _skeleton.transform.position).normalized);
        }
        _skeleton.attackTimer.Start(_skeleton.Data.attackCooldownTime);
        _skeleton.GetComponentInChildren<EnemyAnimation>().OnAnimationTrigger -= Attack;
    }
}