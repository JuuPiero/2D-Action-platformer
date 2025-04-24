
public class PlayerFallState : PlayerState 
{
    public PlayerFallState(string animationBoolName, Player player) : base(animationBoolName, player)
    {
    }

    public override bool IsMatchingConditions()
    {
        return !_player.IsGrounded && _player.RB.velocity.y < 0f && !_player.canLedgeGrab;
    }

}

public class PlayerSwordFallState : PlayerFallState 
{
    public PlayerSwordFallState(string animationBoolName, Player player) : base(animationBoolName, player)
    {
    }
    public override bool IsMatchingConditions()
    {
        return base.IsMatchingConditions() && _player.isAttacking; 
    }
}