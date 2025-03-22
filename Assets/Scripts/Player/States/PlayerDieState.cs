public class PlayerDieState : PlayerState 
{
    public PlayerDieState(string animationName, Player player) : base(animationName, player)
    {
    }
    public override bool IsMatchingConditions() => _player.isDeath;
    
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
    public override bool IsMatchingConditions() => _player.isDeath && _player.isAttacking;
    
    public override void Enter() {
        CanExit = false;
        base.Enter();
    }
}