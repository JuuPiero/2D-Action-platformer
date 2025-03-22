using UnityEngine;

public class Skeleton : Enemy {
    [field: SerializeField] public SkeletonDataSO Data { get; protected set; }
    public Animator Anim { get; protected set; }
    public StateMachine StateMachine { get; protected set; }

    public Rigidbody2D RB  { get; protected set; }

    public CooldownTimer attackTimer;
    public bool canChase;
    public bool canAttack;

    void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        Anim = GetComponentInChildren<Animator>();
        StateMachine = new StateMachine();

        CurrentHealth = Data.maxHealthPoint;
    }

    protected override void Start()
    {
        base.Start();
        StateMachine.AddState(new SkeletonWalkState("Walk", this));
        StateMachine.AddState(new SkeletonAttackState("Attack", this));
        StateMachine.AddState(new SkeletonIdleState("Idle", this));
        attackTimer.Start(Data.attackCooldownTime);
        StateMachine.Initialize();
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

    public override void Damage(int damage)
    {
        attackTimer.Start(Data.attackCooldownTime);
        base.Damage(damage);
    }
   
}