

using UnityEngine;

public class PlayerWinState : PlayerState
{
    public PlayerWinState(string animationName, Player player) : base(animationName, player)
    {
    }

    public override bool IsMatchingConditions()
    {
        return _player.winTrigger;
    }

    public override void Enter()
    {
        base.Enter();
        CanExit = false;
    }

    public override void Update()
    {
        base.Update();
        _player.RB.velocity = Vector2.zero;
    }
   
}