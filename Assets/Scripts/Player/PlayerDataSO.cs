using UnityEngine;
[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Character/Player")]
public class PlayerDataSO : ScriptableObject 
{
    public LayerMask whatIsGround;
    public LayerMask whatIsEnemy;
    public float groundCheckRadius = 0.2f;
    public float wallCheckDistance = 0.3f;

    [Header("Resource")]
    public float maxHealthPoint = 100f;
    public float maxStamina = 100f;
    public float maxMana = 200f;


    [Header("Move")]
    public float speed = 4f;
    public float pushSpeed = 2f;
    public float wallSlideSpeed = 1f;

    [Header("Jump")]
    public float jumpForce = 14f;
    public int maxJumps = 2;
    // public float coyoteTime = 0.15f;    // Thời gian có thể nhảy sau khi rơi
    // public float jumpBufferTime = 0.15f; // Thời gian có thể nhảy nếu nhấn sớm
    // public float variableJumpMultiplier = 0.5f; // Giúp nhảy cao hơn khi giữ phím



    [Header("Dash")]
    public float dashSpeed  = 20f;
    public float dashCooldownTime = 0.8f;
    public float dashTime = 0.3f;


    [Header("Attack")]
    public float combatTime = 5f;
    public float attackCooldownTime = 1f;
    public float shieldBashCooldownTime = 2f;
    public float stunTime = 0.8f;

    public float knockbackForce = 4f;


    public int basicAttackDamage = 20;
    public int heavyAttackDamage = 20;

}