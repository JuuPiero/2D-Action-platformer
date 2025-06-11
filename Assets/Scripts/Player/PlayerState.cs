
public abstract class PlayerState : State
{
    protected Player _player;
    protected PlayerAnimation _playerAnim;

    public PlayerState(string animationBoolName, Player player) : base(animationBoolName)
    {
        _player = player;
        _playerAnim = _player.GetComponentInChildren<PlayerAnimation>();
    }
    public override void Enter() 
    {
        base.Enter();
        _player.Anim?.SetBool(AnimationBoolName, true);
    }

    public override void Exit() 
    {
        base.Exit();
        _player.Anim?.SetBool(AnimationBoolName, false);
    }

    public override void Update() 
    {
        base.Update();
    }

    public override bool IsMatchingConditions()
    {
        return false;
    }
}