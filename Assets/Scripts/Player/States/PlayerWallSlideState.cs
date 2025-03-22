
using UnityEngine;

public class PlayerWallSlideState : PlayerState 
{
    public PlayerWallSlideState(string animationName, Player player) : base(animationName, player) {}

    public override bool IsMatchingConditions()
    {
        return !_player.IsGrounded && _player.IsTouchingWall && _player.InputHandler.Direction.y == 0f
        && (_player.InputHandler.Direction.x * _player.transform.localScale.x > 0f);
    }

    public override void Enter() 
    {
        base.Enter();
    }
    public override void FixedUpdate() 
    {
        base.FixedUpdate();
        _player.RB.velocity = new Vector2(_player.RB.velocity.x, -_player.Data.wallSlideSpeed);
    }
}