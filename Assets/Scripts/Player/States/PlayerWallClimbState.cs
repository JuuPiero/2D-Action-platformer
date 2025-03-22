
using UnityEngine;

public class PlayerWallClimbState : PlayerState
{
    public PlayerWallClimbState(string animationName, Player player) : base(animationName, player)
    {
    }

    public override bool IsMatchingConditions()
    {
        return _player.IsTouchingWall && _player.InputHandler.Direction.y != 0f;
    }

    public override void FixedUpdate() 
    {
        base.FixedUpdate();
        _player.RB.velocity = new Vector2(0f, 3f * _player.InputHandler.Direction.y);
    }
}