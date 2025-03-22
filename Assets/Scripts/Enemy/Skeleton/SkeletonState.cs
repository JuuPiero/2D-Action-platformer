public abstract class SkeletonState : State {
    protected Skeleton _skeleton;

    public SkeletonState(string animationName, Skeleton skeleton) : base(animationName) 
    {
        _skeleton = skeleton;
    }

    public override bool IsMatchingConditions()
    {
        return false;
    }

    public override void Enter()
    {
        base.Enter();
        _skeleton.Anim.SetBool(AnimationBoolName, true);
    }
    public override void Exit()
    {
        base.Exit();
        _skeleton.Anim.SetBool(AnimationBoolName, false);
    }
}

