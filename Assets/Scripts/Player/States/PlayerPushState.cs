using UnityEngine;

public class PlayerPushState : PlayerState {
    public PlayerPushState(string animationName, Player player) : base(animationName, player) {}

    public override bool IsMatchingConditions()
    {
        return _player.IsGrounded && _player.InputHandler.PushPressed && _player.CanPush && _player.InputHandler.Direction.x != 0f;
    }
    public override void Update() {
        base.Update();
        var pushables = Physics2D.OverlapCircleAll(_player.wallCheck.position, 0.5f, _player.Data.whatIsGround);
        foreach (var pushable in pushables)
        {
            pushable.GetComponent<IPushable>()?.Pushed(_player.transform.localScale, _player.Data.pushSpeed);
            _player.RB.velocity = new Vector2( _player.transform.localScale.x *  _player.Data.pushSpeed, 0f);
            // pushable.GetComponent<Crate>().Pushed(_player.transform.localScale, _player.Data.pushSpeed);
        }
    }
    public override void Exit() {
        var pushables = Physics2D.OverlapCircleAll(_player.wallCheck.position, 0.5f, _player.Data.whatIsGround);
        foreach (var pushable in pushables) {
            var pushObj = pushable.GetComponent<IPushable>();
            if(pushObj != null) {
                pushObj.StopPushing();
            }
        }
        base.Exit();
    }
}
