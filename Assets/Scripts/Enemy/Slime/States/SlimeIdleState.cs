public class SlimeIdleState : SlimeState
{
    public SlimeIdleState(string animationName, Slime slime) : base(animationName, slime) {}

    public override bool IsMatchingConditions()
    {
        return slime.IsGrounded && !slime.canChase;
    }
}

