using UnityEngine;

public class PlayerDieState : PlayerState 
{
    public PlayerDieState(string animationName, Player player) : base(animationName, player)
    {
    }
    public override bool IsMatchingConditions() => _player.Resource.CurrentHealth <= 0;
    
    public override void Enter() {
        CanExit = false;
        base.Enter();
    }
}

public class PlayerSwordDieState : PlayerDieState
{
    public PlayerSwordDieState(string animationName, Player player) : base(animationName, player)
    {
    }
    public override bool IsMatchingConditions() => base.IsMatchingConditions() && _player.isAttacking;

    public override void Enter()
    {
        CanExit = false;
        base.Enter();
    }
    public override void Update()
    {
        base.Update();
        _player.RB.velocity = Vector2.zero;

    }
}