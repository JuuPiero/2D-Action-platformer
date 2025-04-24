
using UnityEngine;

public class PlayerLedgeGrabState : PlayerState
{
    public PlayerLedgeGrabState(string animationName, Player player) : base(animationName, player)
    {
    }

    public override bool IsMatchingConditions()
    {
        return !_player.IsGrounded && _player.canLedgeGrab && Mathf.Round(_player.RB.velocity.y) == 0f && (StateMachine.CurrentState is PlayerFallState || StateMachine.CurrentState is PlayerJumpState);
    }

    public override void Enter() {
        _player.jumpCount = 0;
        base.Enter();
    }
}