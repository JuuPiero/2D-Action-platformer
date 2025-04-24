public class PlayerDieState : PlayerState 
{
    public PlayerDieState(string animationName, Player player) : base(animationName, player)
    {
    }
    public override bool IsMatchingConditions() => _player.CurrentHealth <= 0;
    
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
    
    public override void Enter() {
        CanExit = false;
        base.Enter();
    }
}