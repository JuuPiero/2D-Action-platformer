
using UnityEngine;

public class PlayerLedgeGrabState : PlayerState
{
    public PlayerLedgeGrabState(string animationName, Player player) : base(animationName, player)
    {
    }

    public override bool IsMatchingConditions()
    {
        return _player.canLedgeGrab && !_player.IsGrounded;
    }

    public override void Enter() {
        base.Enter();
        CanExit = false;
    }
    public override void FixedUpdate() {
        base.FixedUpdate();
        _player.RB.velocity = Vector2.zero;
    }
}