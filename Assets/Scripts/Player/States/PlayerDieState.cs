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

public class PlayerSwordDieState : PlayerState 
{
    public PlayerSwordDieState(string animationName, Player player) : base(animationName, player)
    {
    }
    public override bool IsMatchingConditions() => _player.CurrentHealth <= 0 && _player.isAttacking;
    
    public override void Enter() {
        CanExit = false;
        base.Enter();
    }
}