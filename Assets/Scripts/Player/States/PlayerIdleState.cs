using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(string animationName, Player player) : base(animationName, player) {}

    public override bool IsMatchingConditions()
    {
        return _player.IsGrounded && _player.InputHandler.Direction.x == 0f;
    }
}

public class PlayerSwordIdleState : PlayerState
{
    public PlayerSwordIdleState(string animationName, Player player) : base(animationName, player) {}

    public override bool IsMatchingConditions()
    {
        return _player.IsGrounded && _player.InputHandler.Direction.x == 0f && _player.isAttacking;
    }
}
