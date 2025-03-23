using UnityEngine;

public class PlayerShieldUpState : PlayerState 
{
    public PlayerShieldUpState(string animationName, Player player) : base(animationName, player) {}
    public override bool IsMatchingConditions()
    {
        return _player.IsGrounded && _player.InputHandler.IsShieldPressed;
    }
}

public class PlayerShieldIdleState : PlayerState
{
    public PlayerShieldIdleState(string animationName, Player player) : base(animationName, player) {}

    GameObject _shield = null;
    CooldownTimer _createShieldTimer;

    public override bool IsMatchingConditions() {
        return _player.IsGrounded && _player.InputHandler.IsHoldingShield;
    }
    public override void Enter() {
        base.Enter();
        _createShieldTimer.Start(0.8f);
    }

    public override void Exit() 
    {
        base.Exit();
        if(_shield != null) {
            GameObject.Destroy(_shield);
        }
    }

    public override void Update()
    {
        base.Update();
        _createShieldTimer.Update(Time.deltaTime);

        if(_shield == null && _createShieldTimer.IsReady) {
            _shield = VFXManager.Instance?.InstantiateEffect("Shield", _player.transform.position);
        }
        if(_shield != null) {
            _shield.transform.position = _player.transform.position;
        }
    }
  
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        _player.RB.velocity = Vector2.zero;
    }
}

public class PlayerShieldBashState : PlayerState 
{
    public PlayerShieldBashState(string animationName, Player player) : base(animationName, player) {}

    public override bool IsMatchingConditions()
    {
        return _player.IsGrounded && _player.InputHandler.IsShieldRelease && _player.isTriggerShieldBash;
    }

    public override void Enter()
    {
        _player.GetComponentInChildren<PlayerAnimation>().OnAnimationTrigger += ShieldBash;
        CanExit = false;
        base.Enter();
        AudioManager.Instance?.PlaySFX("PlayerAttack");
    }

    public void ShieldBash() 
    {
        var enemies = DetectEnemies();
        if(enemies.Length > 0) {
            AudioManager.Instance?.PlaySFX("ShieldBashSFX");
        }
        foreach (var enemy in enemies)
        {
            VFXManager.Instance?.PlayEffect("HitVFX", enemy.transform.position, 0.3f);
            enemy.GetComponent<IDamageable>()?.Damage(10);
            Vector2 direction = _player.transform.localScale.normalized;
            enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction.x * 3f, 3f), ForceMode2D.Impulse);
        }
        _player.shieldBashTimer.Start(_player.Data.shieldBashCooldownTime);
        _player.GetComponentInChildren<PlayerAnimation>().OnAnimationTrigger -= ShieldBash;
        CanExit = true;
    }

    public Collider2D[] DetectEnemies() 
    {
        var hitboxes = _player.GetComponent<PlayerHitboxManager>().hitboxes;
        return Physics2D.OverlapCircleAll(hitboxes[0].position, 0.4f, _player.Data.whatIsEnemy);
    }
}