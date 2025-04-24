using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Animator Anim { get; private set; }

    [field: SerializeField] public PlayerDataSO Data { get; private set; }
    public StateMachine StateMachine { get; private set; }

    public Transform groundCheck;
    public Transform wallCheck;
    public Transform ledgeCheck;

    [field: SerializeField] public Inventory Inventory { get; private set; }

    public Dictionary<EquipmentSlot, ItemDataSO> equippedItems = new();

    public void Equip(EquipmentDataSO equipment)
    {
        equippedItems[equipment.slot] = equipment;
        Debug.Log($"{this.name} equipped {equipment.itemName} in {equipment.slot}");
        Debug.Log(equippedItems);
    }

    [Header("Status")]
    public bool isFacingRight = true;
    [SerializeField] private bool _isGrounded;
    public bool isTriggerShieldBash = false; //COUNTER ATTACK

    public bool IsGrounded => _isGrounded;
    [SerializeField] private bool _isTouchingWall;
    public bool IsTouchingWall => _isTouchingWall;
    [SerializeField] private bool _isTouchingLedge;
    public bool IsTouchingLedge => _isTouchingLedge;

    [field: SerializeField] public int CurrentHealth { get ; set; }
    public float CurrentStamina { get ; set; }
    public float CurrentMana { get ; set; }

    [field: SerializeField] public bool CanPush { get; set; } = false;
    
    public bool canLedgeGrab = false;


    [Header("Attack")]
    public bool isAttacking;
    public bool isStuning;
    [Header("CD")]
    [SerializeField] public CooldownTimer combatCooldown;
    [SerializeField] public CooldownTimer attackCooldown;
    [SerializeField] public CooldownTimer shieldBashTimer;
    [SerializeField] public CooldownTimer spellCastTimer;
    [SerializeField] public CooldownTimer dashTimer;


    [Header("Data")]
    public int jumpCount = 0;
    public GameObject afterimagePrefab;


    void Awake()
    {
        InputHandler = GetComponentInChildren<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        Anim = GetComponentInChildren<Animator>();
        StateMachine = new StateMachine();
        Inventory = new Inventory();
    }

    void Start()
    {
        //SORTING STATE BY PRIORYTY
        StateMachine.AddState(new PlayerIdleState("PlayerIdle", this));
        StateMachine.AddState(new PlayerSwordIdleState("PlayerSwordIdle", this));
        StateMachine.AddState(new PlayerWalkState("PlayerWalk", this));
        StateMachine.AddState(new PlayerSwordWalkState("PlayerSwordWalk", this));
        StateMachine.AddState(new PlayerFallState("PlayerFall", this));
        StateMachine.AddState(new PlayerSwordFallState("PlayerSwordFall", this));
        StateMachine.AddState(new PlayerLandState("PlayerLand", this));
        StateMachine.AddState(new PlayerSwordLandState("PlayerSwordLand", this));

        StateMachine.AddState(new PlayerDashState("PlayerDash", this));
        StateMachine.AddState(new PlayerLedgeGrabState("PlayerLedgeGrab", this));

        StateMachine.AddState(new PlayerShieldUpState("PlayerShieldUp", this));
        StateMachine.AddState(new PlayerShieldIdleState("PlayerShieldIdle", this));
        StateMachine.AddState(new PlayerShieldBashState("PlayerShieldBash", this));

        StateMachine.AddState(new PlayerPushState("PlayerPush", this));
        StateMachine.AddState(new PlayerDieState("PlayerDie", this));
        StateMachine.AddState(new PlayerJumpState("PlayerJump", this));
        StateMachine.AddState(new PlayerSwordJumpState("PlayerSwordJump", this));
        StateMachine.AddState(new PlayerSpellCastingState("PlayerSpellCasting", this));
      
       
        StateMachine.AddState(new PlayerLightAttackState("PlayerAttack", this));
        StateMachine.AddState(new PlayerHeavyAttackState("PlayerHeavyAttack", this));
        // StateMachine.AddState(new PlayerWallSlideState("PlayerWallSlide", this));
        StateMachine.AddState(new PlayerStunState("PlayerStun", this));


        StateMachine.Initialize(StateMachine.GetState<PlayerFallState>());

        CurrentHealth = Data.maxHealthPoint;
        CurrentStamina = Data.maxStamina;
        CurrentMana = Data.maxMana;
    }
  
    void Update() {
        CheckFlip();
        CheckCollision();

        
        if (_isGrounded && RB.velocity.y <= 0) {
            jumpCount = 0;
        }

        if (InputHandler.JumpPressed) {
            Jump();
        }

        
        isAttacking = !combatCooldown.IsReady;
        if(!isAttacking) PlayerLightAttackState.AttackIndex = 1;
   
        
        attackCooldown.Update(Time.deltaTime);
        combatCooldown.Update(Time.deltaTime);
        shieldBashTimer.Update(Time.deltaTime);
        spellCastTimer.Update(Time.deltaTime);
        dashTimer.Update(Time.deltaTime);

        StateMachine.Update();
    }

    void CheckCollision() {
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, Data.groundCheckRadius, Data.whatIsGround);
        _isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, Data.wallCheckDistance * transform.localScale.x, Data.whatIsGround);
        _isTouchingLedge = Physics2D.Raycast(ledgeCheck.position, transform.right, Data.wallCheckDistance * transform.localScale.x, Data.whatIsGround);
        canLedgeGrab = IsTouchingWall && !IsTouchingLedge;
    }

 
    void FixedUpdate() {
        if(!StateMachine.CurrentIs<PlayerDashState>() && !StateMachine.CurrentIs<PlayerLedgeGrabState>()) {
            RB.velocity = new Vector2(Data.speed * InputHandler.Direction.x, RB.velocity.y);
        }
        StateMachine.FixedUpdate();
    }
    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(groundCheck.position, Data.groundCheckRadius);
        Gizmos.DrawLine(wallCheck.position,
            new Vector3((wallCheck.position.x + Data.wallCheckDistance * transform.localScale.x),
            wallCheck.position.y,
            wallCheck.position.z));

        Gizmos.DrawLine(ledgeCheck.position,
            new Vector3((ledgeCheck.position.x + Data.wallCheckDistance * transform.localScale.x),
            ledgeCheck.position.y,
            ledgeCheck.position.z));
    }

    public void Jump() {
        if(jumpCount < Data.maxJumps) {
            jumpCount++;
            RB.velocity = new Vector2(RB.velocity.x, Data.jumpForce);
        }
    }

    public void CheckFlip() {
        var s = transform.localScale;

        if(isFacingRight && InputHandler.Direction.x < 0f) {
            isFacingRight = !isFacingRight;
            s.x *= -1;
            transform.localScale = s;
        }
        else if(!isFacingRight && InputHandler.Direction.x > 0f) {
            isFacingRight = !isFacingRight;
            s.x *= -1;
            transform.localScale = s;
        }
    }
    private IEnumerator ResetShieldBashTrigger() {
        yield return new WaitForSeconds(0.5f);
        isTriggerShieldBash = false;
    }
    

    public void Damage(int damage) {
        
        if(StateMachine.CurrentState is PlayerShieldIdleState) {
            AudioManager.Instance?.PlaySFX("ShieldGuard");
            isTriggerShieldBash = true;
            StartCoroutine(ResetShieldBashTrigger());
            return;
        }
        
        CurrentHealth -= damage;
        isStuning = true;

        if(CurrentHealth <= 0) {
            Die();
        }
    }

    public void Die() => CurrentHealth = 0; 
}
