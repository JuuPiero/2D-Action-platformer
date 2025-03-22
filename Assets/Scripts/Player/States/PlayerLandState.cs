public class PlayerLandState : PlayerState 
{
    public PlayerLandState(string animationName, Player player) : base(animationName, player) {}

    public override bool IsMatchingConditions()
    {
        return _player.IsGrounded && (StateMachine.CurrentState is PlayerFallState || StateMachine.CurrentState is PlayerSwordFallState);
    }
}

public class PlayerSwordLandState : PlayerState 
{
    public PlayerSwordLandState(string animationName, Player player) : base(animationName, player) {}

    public override bool IsMatchingConditions()
    {
        return _player.IsGrounded && (StateMachine.CurrentState is PlayerFallState || StateMachine.CurrentState is PlayerSwordFallState) && _player.isAttacking;
    }
}