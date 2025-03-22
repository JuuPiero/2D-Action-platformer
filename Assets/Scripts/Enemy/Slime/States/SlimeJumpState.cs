public class SlimeJumpState : SlimeState
{
    public SlimeJumpState(string animationName, Slime slime) : base(animationName, slime) {}

    public override bool IsMatchingConditions()
    {
        return !slime.IsGrounded;
    }
}

