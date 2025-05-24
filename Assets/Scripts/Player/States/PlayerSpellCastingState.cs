
using UnityEngine;

public class PlayerSpellCastingState : PlayerState 
{
    public const float MANA = 20;

    public PlayerSpellCastingState(string animationName, Player player) : base(animationName, player) {}

    public override bool IsMatchingConditions()
    {
        return _player.IsGrounded && _player.InputHandler.IsSpellCastPressed && _player.spellCastTimer.IsReady;
    }

    public override void Enter()
    {
        _player.GetComponentInChildren<PlayerAnimation>().OnAnimationTrigger += SpellCast;
        //_player.transform.Find("Aura").gameObject.SetActive(true);
        CanExit = false;
        base.Enter();
        // AudioManager.Instance?.PlaySFX("PlayerAttack");
    }

    public void SpellCast() 
    {
        var enemies = DetectEnemies();
        foreach (var enemy in enemies)
        {
            Vector3 effectPos = new Vector3(enemy.transform.position.x, _player.groundCheck.position.y, 0);
            VFXManager.Instance?.PlayEffect("Lightning", effectPos, 0.5f);
            AudioManager.Instance?.PlaySFX("LightningSFX");
            enemy.GetComponent<IDamageable>()?.Damage(60);
            Vector2 direction = _player.transform.localScale.normalized;
            enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction.x * 3f, 3f), ForceMode2D.Impulse);
        }
        CanExit = true;
        _player.spellCastTimer.Start(2f);
        _player.GetComponentInChildren<PlayerAnimation>().OnAnimationTrigger -= SpellCast;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        _player.RB.velocity = Vector2.zero;
    }

    public Collider2D[] DetectEnemies() 
    {
        return Physics2D.OverlapCircleAll(_player.transform.position, 5f, _player.Data.whatIsEnemy);
    }
}