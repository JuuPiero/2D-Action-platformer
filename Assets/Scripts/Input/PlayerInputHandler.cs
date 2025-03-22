using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputHandler : MonoBehaviour
{
    private const float HOLD_THRESHOLD = 0.25f;
    private PlayerControls _controls;

    private Vector2 _inputDirection;
    public Vector2 Direction => _inputDirection;

    private bool _isSpellCastPressed;
    public bool IsSpellCastPressed => _isSpellCastPressed;

    private bool _isDashPressed;
    public bool IsDashPressed => _isDashPressed;

    private float _shieldPressTime;
    private bool _isHoldingShield;
    private bool _isShieldPressed;
    private bool _isShieldRelease;
    public bool IsHoldingShield => _isHoldingShield;
    public bool IsShieldPressed => _isShieldPressed;
    public bool IsShieldRelease => _isShieldRelease;


    private float _attackPressTime;
    private bool _isHoldingAttack;
    private bool _isAttackPressed;
    private int _attackComboIndex = 0;
    public bool IsHoldingAttack => _isHoldingAttack;
    public bool IsAttackPressed => _isAttackPressed;
    public int AttackComboIndex => _attackComboIndex;


    private bool _jumpPressed;
    public bool JumpPressed => _jumpPressed;
    public bool JumpReleased { get; private set; }

    private void Awake()
    {
        _controls = new PlayerControls();
    }

    private void OnEnable()
    {
        _controls.GamePlay.Enable();

        _controls.GamePlay.Move.performed += ctx => _inputDirection = ctx.ReadValue<Vector2>();
        _controls.GamePlay.Move.canceled += ctx => _inputDirection = Vector2.zero;

        _controls.GamePlay.Shield.started += ctx => StartShield();
        _controls.GamePlay.Shield.canceled += ctx => ReleaseShield();

        _controls.GamePlay.Jump.started += ctx => _jumpPressed = true;
        _controls.GamePlay.Jump.canceled += ctx => _jumpPressed = false;

        _controls.GamePlay.Dash.started += ctx => _isDashPressed = true;
        _controls.GamePlay.Dash.canceled += ctx => _isDashPressed = false;

        _controls.GamePlay.SpellCast.started += ctx => _isSpellCastPressed = true;
        _controls.GamePlay.SpellCast.canceled += ctx => _isSpellCastPressed = false;

        _controls.GamePlay.Attack.started += ctx => StartAttack();
        _controls.GamePlay.Attack.canceled += ctx => ReleaseAttack();

    }

    private void OnDisable()
    {
        _controls.GamePlay.Disable();
    }
    private void StartAttack() {
        _attackPressTime = Time.time;
        _isAttackPressed = true;
        _isHoldingAttack = false;
    }

    private void ReleaseAttack() {
        float holdDuration = Time.time - _attackPressTime;

        if (holdDuration >= HOLD_THRESHOLD) {
            _isHoldingAttack = false;
        }
        _isAttackPressed = false;
    }

    private void StartShield() {
        _shieldPressTime = Time.time;
        _isShieldPressed = true;
        _isHoldingShield = false;
        _isShieldRelease = false;
    }

    private void ReleaseShield() {
        float holdDuration = Time.time - _shieldPressTime;

        if (_isHoldingShield) {
            _isShieldRelease = true;
            _isShieldPressed = false;
            _isHoldingShield = false;
            Invoke(nameof(ResetShieldState), 0.3f);
        }
        else {
            // Nếu nhả sớm → Không có tấn công, trở về trạng thái bình thường
            ResetShieldState();
        }
    }

    private void ResetShieldState() {
        _isShieldPressed = false;
        _isShieldRelease = false;
        _isHoldingShield = false;
    }

    void Update()
    {
        if (_isShieldPressed && (Time.time - _shieldPressTime) >= HOLD_THRESHOLD)
        {
            _isHoldingShield = true;
        } 

        if (_isAttackPressed && (Time.time - _attackPressTime) >= HOLD_THRESHOLD) {
            _isHoldingAttack = true; // Chuyển sang heavy attack nếu giữ lâu
            _isAttackPressed = false;
        }

        _jumpPressed = _controls.GamePlay.Jump.WasPressedThisFrame();
    }
}
