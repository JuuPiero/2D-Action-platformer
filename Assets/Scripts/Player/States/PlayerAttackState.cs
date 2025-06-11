using System.Collections;
using UnityEngine;


public class PlayerHeavyAttackState : PlayerState {
    public PlayerHeavyAttackState(string animationName, Player player) : base(animationName, player) {
    }
    ~PlayerHeavyAttackState() {
    }

    public override bool IsMatchingConditions()
    {
        return _player.attackCooldown.IsReady && _player.InputHandler.IsHoldingAttack && _player.Resource.CurrentStamina > 0f;
    }

    public override void Enter() {
        _playerAnim.OnAttack += Attack;
        base.Enter();
        // AudioManager.Instance?.PlaySFX("PlayerAttack");
    }

    public override void Update()
    {
        base.Update();
        // _player.Re CurrentStamina -= Time.deltaTime;
        // if(_player.CurrentStamina < 0f) _player.CurrentStamina = 0f;
        
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        _player.RB.velocity = new Vector2((_player.Data.speed / 2) * _player.InputHandler.Direction.x, _player.RB.velocity.y);
    }

    public override void Exit() {
        base.Exit();
        
        _player.attackCooldown.Start(_player.Data.attackCooldownTime);
        _playerAnim.OnAttack -= Attack;
    }

    void Attack()
    {
        // if(!AudioManager.Instance.sfxSource.isPlaying)
        //     AudioManager.Instance?.PlaySFX("SwordSwoosh");
        Collider2D[] hitEnemies = DetectEnemies();
        foreach (var enemy in hitEnemies)
        {
            VFXManager.Instance?.PlayEffect("HitVFX", enemy.transform.position, 0.3f);
            AudioManager.Instance?.PlaySFX("SwordSFX_" + 4);
            enemy.GetComponent<IDamageable>()?.Damage(20);
            CameraShake.Instance?.Shake(1.2f, 0.3f);
            Debug.Log(enemy.gameObject.name);
        }
       
    }

    public Collider2D[] DetectEnemies() {
        var hitboxes = _player.GetComponent<PlayerHitboxManager>().hitboxes;
        return Physics2D.OverlapBoxAll(hitboxes[3].position, new Vector2(3f, 1f), 0f, _player.Data.whatIsEnemy);
    }
}


public class PlayerLightAttackState : PlayerState {
    public static byte AttackIndex;
    private float _speed;
    private readonly string AnimationBaseName;
    public PlayerLightAttackState(string animationName, Player player) : base(animationName, player) {
        AnimationBaseName = animationName;
        AttackIndex = 1;
        _speed = _player.Data.speed;
    }

    public override bool IsMatchingConditions() {
        return _player.attackCooldown.IsReady && _player.InputHandler.IsAttackPressed && !_player.InputHandler.IsHoldingAttack;
    }

    public override void Enter() {
        _player.combatCooldown.Start(_player.Data.combatTime);
        _playerAnim.OnAttack += Attack;
        CanExit = false;
       
        _player.RB.velocity = new Vector2(1f, 0f);
        if(AttackIndex > 3) {
            AttackIndex = 1;
        }
        AnimationBoolName = AnimationBaseName + "_" + AttackIndex;
        AudioManager.Instance?.PlaySoundOneShot(AnimationBoolName, _player.transform.position);
        // AudioManager.Instance?.PlaySFX("PlayerAttack");
        base.Enter();
    }


    public override void Exit() {
        base.Exit();
        AttackIndex++;
        _player.attackCooldown.Start(_player.Data.attackCooldownTime);
        // _player.Data.speed = _speed;
    }

    void Attack() {
        Collider2D[] hitEnemies = DetectEnemies();
        var hitboxes = _player.GetComponent<PlayerHitboxManager>().hitboxes;
        foreach (var enemy in hitEnemies) {
            enemy.GetComponent<IDamageable>()?.Damage(20);
            VFXManager.Instance?.PlayEffect("HitVFX", hitboxes[0].position, 0.3f);
            AudioManager.Instance?.PlaySFX("SwordSFX_" + AttackIndex);
            CameraShake.Instance?.Shake(1.5f, 0.3f);
            Debug.Log(enemy.gameObject.name);
        }
        _playerAnim.OnAttack -= Attack;
        CanExit = true;
    }

    public Collider2D[] DetectEnemies() {
        var hitboxes = _player.GetComponent<PlayerHitboxManager>().hitboxes;
        return Physics2D.OverlapCircleAll(hitboxes[0].position, 0.4f, _player.Data.whatIsEnemy);
    }
}