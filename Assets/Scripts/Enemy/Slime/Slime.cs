
using UnityEngine;

public class Slime : Enemy
{
    [field: SerializeField] public SlimeDataSO Data { get; private set; }
    [field: SerializeField] public StateMachine StateMachine { get; private set; }
    [field: SerializeField] public Animator Anim { get; private set; }
    [field: SerializeField] public Rigidbody2D RB { get; private set; }

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;
    

    [SerializeField] private CooldownTimer jumpCooldown;
    private CooldownTimer attackCooldown;
    
    public bool canChase;
    public bool canAttack;
    [field: SerializeField] public bool IsGrounded { get; protected set; }
  


    void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        Anim = GetComponentInChildren<Animator>();
        StateMachine = new StateMachine();
        StateMachine.AddState(new SlimeIdleState("Idle", this));
        StateMachine.AddState(new SlimeJumpState("Jump", this));
        StateMachine.Initialize();
    }

    protected override void Start()
    {
        base.Start();
        CurrentHealth = Data.maxHealthPoint;
    }
    protected override void Update()
    {
        base.Update();
        IsGrounded = Physics2D.OverlapCircle(groundCheck.position, Data.groundCheckRadius, whatIsGround);
        canAttack = Physics2D.OverlapCircle(transform.position, Data.attackRadius, playerLayer);
        canChase = Physics2D.OverlapCircle(transform.position, Data.chaseRadius, playerLayer) && !canAttack;
       
        jumpCooldown.Update(Time.deltaTime);
        attackCooldown.Update(Time.deltaTime);

        if (canChase) {
            Jump();
        }
        if(canAttack) {
            Attack();
        }

        StateMachine.Update();
    }

 
    void Jump()
    {
        if(IsGrounded && jumpCooldown.IsReady) {
            Direction = GetDirectionToPlayer();
            Vector2 jumpVelocity = new Vector2(Direction.x * Data.jumpForce.x, Data.jumpForce.y);
            RB.velocity = jumpVelocity;
            jumpCooldown.Start(Data.jumpCoolDownTime);
        }
    }
    void Attack() 
    {
        if(attackCooldown.IsReady) 
        {
            Direction = GetDirectionToPlayer();
            RB.velocity = new Vector2(Data.attackForce.x * Direction.x, Data.attackForce.y);
            Collider2D player = Physics2D.OverlapCircle(attackPoint.position, Data.damageRadius, playerLayer);
            if(player != null) {
                Debug.Log(player.gameObject.name);
                player.GetComponent<IDamageable>().Damage(Data.damage);
            }

            attackCooldown.Start(Data.attackCoolDownTime);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Data.chaseRadius);


        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Data.attackRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheck.position, Data.groundCheckRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint.position, Data.damageRadius);
    }
    
    public override void Damage(int damage)
    {
        attackCooldown.Start(Data.attackCoolDownTime);
        base.Damage(damage);
    }
}