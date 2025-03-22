
public class PlayerFallState : PlayerState 
{
    public PlayerFallState(string animationName, Player player) : base(animationName, player)
    {
    }

    public override bool IsMatchingConditions()
    {
        return !_player.IsGrounded && _player.RB.velocity.y < 0f;
    }

}

public class PlayerSwordFallState : PlayerState 
{
    public PlayerSwordFallState(string animationName, Player player) : base(animationName, player)
    {
    }
    public override bool IsMatchingConditions()
    {
        return !_player.IsGrounded && _player.RB.velocity.y <= 0f && _player.isAttacking; 
    }
}