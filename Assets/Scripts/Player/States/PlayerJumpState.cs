public class PlayerJumpState : PlayerState 
{
    public PlayerJumpState(string animationName, Player player) : base(animationName, player)
    {
    }

    public override bool IsMatchingConditions()
    {
        return !_player.IsGrounded && _player.RB.velocity.y > 0.1f;
        // return _player.InputHandler.JumpPressed;
    }

    public override void Enter() {
        // AudioManager.Instance.PlaySFX("PlayerJump");
        // _player.Jump();
        VFXManager.Instance?.PlayEffect("JumpVFX", _player.groundCheck.position, 0.3f);
        base.Enter();
    }
}



public class PlayerSwordJumpState : PlayerJumpState 
{
    public PlayerSwordJumpState(string animationName, Player player) : base(animationName, player)
    {
    }
    public override void Enter() {
        // AudioManager.Instance.PlaySFX("PlayerJump");
        VFXManager.Instance?.PlayEffect("JumpVFX", _player.groundCheck.position, 0.3f);
        base.Enter();
    }

    public override bool IsMatchingConditions()
    {
        return base.IsMatchingConditions() && _player.isAttacking;
    }
}