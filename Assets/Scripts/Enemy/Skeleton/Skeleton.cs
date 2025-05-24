using System.Collections;
using UnityEngine;

public class Skeleton : Enemy, IKnockbackable {
    [field: SerializeField] public SkeletonDataSO Data { get; protected set; }
    public Animator Anim { get; protected set; }
    public StateMachine StateMachine { get; protected set; }

    public Rigidbody2D RB  { get; protected set; }

    public Vector2 startPosition;

    public CooldownTimer attackTimer;
    public bool canChase;
    public bool canAttack;
    public bool isKnockedBack;

    void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        Anim = GetComponentInChildren<Animator>();
        StateMachine = new StateMachine();

        CurrentHealth = Data.maxHealthPoint;

        startPosition = transform.position;
    }

    protected override void Start()
    {
        base.Start();
        StateMachine.AddState(new SkeletonWalkState("Walk", this));
        StateMachine.AddState(new SkeletonAttackState("Attack", this));
        StateMachine.AddState(new SkeletonIdleState("Idle", this));
        attackTimer.Start(Data.attackCooldownTime);
        StateMachine.Initialize(StateMachine.GetState<SkeletonIdleState>());
    }

    protected override void Update() {
        base.Update();
        StateMachine.Update();
    }

    void FixedUpdate()
    {
        canAttack = Physics2D.OverlapCircle(attackPoint.position, Data.attackRadius, playerLayer);
        canChase = Physics2D.OverlapCircle(transform.position, Data.detectionRadius, playerLayer) && !canAttack;
        
        if(canChase) {
            Vector2 playerPos = _targetPlayer.transform.position;
            float deltaX = Mathf.Abs(playerPos.x - transform.position.x);

            // Chỉ đổi hướng nếu khoảng cách X đủ lớn để tránh quay liên tục
            if (deltaX < 0.5f)
            {
                return;
            }
            Direction = GetDirectionToPlayer();
            RB.velocity = new Vector2(Direction.x * Data.chaseSpeed, RB.velocity.y);
        }

        StateMachine.FixedUpdate();
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, Data.detectionRadius);
        Gizmos.DrawWireSphere(attackPoint.position, Data.attackRadius);
    }

    public override void Damage(float damage) {
        Vector2 knockbackDir = (transform.position - _targetPlayer.transform.position).normalized;
        ApplyKnockback(knockbackDir, Data.knockbackForce, 0.2f);
        attackTimer.Start(Data.attackCooldownTime);
        base.Damage(damage);
    }

    public void ApplyKnockback(Vector2 direction, float force, float duration)
    {
        if (isKnockedBack) return;

        isKnockedBack = true;
        RB.velocity = Vector2.zero; // Reset trước khi áp lực mới
        RB.AddForce(direction.normalized * force, ForceMode2D.Impulse);

        StartCoroutine(KnockbackCoroutine(duration));
    }

    private IEnumerator KnockbackCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        isKnockedBack = false;
        RB.velocity = Vector2.zero;
    }
}