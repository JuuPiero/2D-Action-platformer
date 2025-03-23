
using UnityEngine;

public class SkeletonIdleState : SkeletonState
{
    public SkeletonIdleState(string animationName, Skeleton skeleton) : base(animationName, skeleton)
    {}


    public override bool IsMatchingConditions()
    {
        // return false;
        return (_skeleton.canAttack && !_skeleton.attackTimer.IsReady) 
        || (!_skeleton.canChase && !_skeleton.canAttack);
    }

    public override void Enter() {
        base.Enter();
        //_skeleton.attackTimer.Start(_skeleton.Data.attackCooldownTime / 2);
    }

    public override void Update()
    {
        base.Update();
        _skeleton.attackTimer.Update(Time.deltaTime);
    }
}