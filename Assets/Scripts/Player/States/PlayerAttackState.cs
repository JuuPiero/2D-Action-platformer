using System.Collections;
using UnityEngine;


public class PlayerHeavyAttackState : PlayerState  {
    public PlayerHeavyAttackState(string animationName, Player player) : base(animationName, player) {
    }
    ~PlayerHeavyAttackState() {
    }

    public override bool IsMatchingConditions()
    {
        return _player.attackCooldown.IsReady && _player.InputHandler.IsHoldingAttack && _player.CurrentStamina > 0f; // ADD STAMINA CONDITION
    }

    public override void Enter() {
        _player.GetComponentInChildren<PlayerAnimation>().OnAttack += Attack;
        base.Enter();
        AudioManager.Instance?.PlaySFX("PlayerAttack");
    }

    public override void Update()
    {
        base.Update();
        _player.CurrentStamina -= Time.deltaTime;
        if(_player.CurrentStamina < 0f) _player.CurrentStamina = 0f;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        _player.RB.velocity = new Vector2((_player.Data.speed / 2) * _player.InputHandler.Direction.x, _player.RB.velocity.y);
    }

    public override void Exit() {
        base.Exit();
        _player.attackCooldown.Start(_player.Data.attackCooldownTime);
        _player.GetComponentInChildren<PlayerAnimation>().OnAttack -= Attack;
    }

    void Attack() {
        Collider2D[] hitEnemies = DetectEnemies();
        foreach (var enemy in hitEnemies)
        {
            VFXManager.Instance?.PlayEffect("HitVFX", enemy.transform.position, 0.3f);
            AudioManager.Instance?.PlaySFX("SwordSFX_" + 4);
            enemy.GetComponent<IDamageable>()?.Damage(20);
            Debug.Log(enemy.gameObject.name);
        }
    }

    public Collider2D[] DetectEnemies() {
        var hitboxes = _player.GetComponent<PlayerHitboxManager>().hitboxes;
        return Physics2D.OverlapBoxAll(hitboxes[3].position, new Vector2(3f, 1f), 0f, _player.Data.whatIsEnemy);
    }
}


public class PlayerLightAttackState : PlayerState {
    public static byte attackIndex;

    private readonly string AnimationBaseName;
    public PlayerLightAttackState(string animationName, Player player) : base(animationName, player) {
        AnimationBaseName = animationName;
        attackIndex = 1;
    }

    public override bool IsMatchingConditions() {
        return _player.attackCooldown.IsReady && _player.InputHandler.IsAttackPressed && !_player.InputHandler.IsHoldingAttack;
    }

    public override void Enter() {
        _player.combatCooldown.Start(_player.Data.combatTime);
        _player.GetComponentInChildren<PlayerAnimation>().OnAttack += Attack;
        CanExit = false;
        if(attackIndex > 3) {
            attackIndex = 1;
        }
        AnimationBoolName = AnimationBaseName + "_" + attackIndex;
        base.Enter();
        AudioManager.Instance?.PlaySFX("PlayerAttack");
    }


    public override void Exit() {
        base.Exit();
        attackIndex++;
        _player.GetComponentInChildren<PlayerAnimation>().OnAttack -= Attack;
        _player.attackCooldown.Start(_player.Data.attackCooldownTime);
    }

    void Attack() {
        Collider2D[] hitEnemies = DetectEnemies();
        var hitboxes = _player.GetComponent<PlayerHitboxManager>().hitboxes;
        // VFXManager.Instance.PlayEffect("Slash_1", hitboxes[0].transform.position, 0.2f);
        foreach (var enemy in hitEnemies)
        {
            VFXManager.Instance?.PlayEffect("HitVFX", hitboxes[0].position, 0.3f);
            AudioManager.Instance?.PlaySFX("SwordSFX_" + attackIndex);
            enemy.GetComponent<IDamageable>()?.Damage(20);
            Debug.Log(enemy.gameObject.name);
        }
        CanExit = true;
    }

    public Collider2D[] DetectEnemies() {
        var hitboxes = _player.GetComponent<PlayerHitboxManager>().hitboxes;
        return Physics2D.OverlapCircleAll(hitboxes[0].position, 0.4f, _player.Data.whatIsEnemy);
    }
}