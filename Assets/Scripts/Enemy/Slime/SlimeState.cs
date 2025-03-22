public abstract class SlimeState : State
{
    protected Slime slime;

    public SlimeState(string animationName, Slime slime) : base(animationName) 
    {
        this.slime = slime;
    }

    public override bool IsMatchingConditions()
    {
        return false;
    }

    public override void Enter()
    {
        base.Enter();
        slime.Anim.SetBool(AnimationBoolName, true);
    }
    public override void Exit()
    {
        base.Exit();
        slime.Anim.SetBool(AnimationBoolName, false);
    }
}

