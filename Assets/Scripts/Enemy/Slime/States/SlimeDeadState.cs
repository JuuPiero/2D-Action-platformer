public class SlimeDeadState : SlimeState
{
    public SlimeDeadState(string animationName, Slime slime) : base(animationName, slime) {}

    public override bool IsMatchingConditions()
    {
        return slime.CurrentHealth <= 0f;
    }
    
}

