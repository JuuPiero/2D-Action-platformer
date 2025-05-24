using UnityEngine;

public class PlayerPushState : PlayerState {
    public PlayerPushState(string animationName, Player player) : base(animationName, player) {}

    public override bool IsMatchingConditions()
    {
        return _player.IsGrounded && _player.InputHandler.InteractPressed && _player.CanPush && _player.InputHandler.Direction.x != 0f;
    }
    public override void Update() {
        base.Update();
        var pushables = Physics2D.OverlapCircleAll(_player.wallCheck.position, 0.5f, _player.Data.whatIsGround);
        foreach (var pushable in pushables)
        {
            pushable.GetComponent<IPushable>()?.Pushed(_player.transform.localScale, _player.Data.pushSpeed);
            _player.RB.velocity = new Vector2( _player.InputHandler.Direction.x *  _player.Data.pushSpeed, 0f);
            // pushable.GetComponent<Crate>().Pushed(_player.transform.localScale, _player.Data.pushSpeed);
        }
    }
    public override void Exit() {
        var pushables = Physics2D.OverlapCircleAll(_player.wallCheck.position, 0.5f, _player.Data.whatIsGround);
        foreach (var pushable in pushables) {
            var pushedObj = pushable.GetComponent<IPushable>();
            pushedObj?.StopPushing();
        }
        base.Exit();
    }
}
