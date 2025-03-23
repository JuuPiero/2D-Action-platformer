using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable {
    [SerializeField] public LayerMask playerLayer;
    [SerializeField] protected Player _targetPlayer;

    public Transform attackPoint;


    [field: SerializeField] public int CurrentHealth { get; set; } = 100;
    [field: SerializeField] public Vector2 Direction { get; set; }
    public bool IsFacingRight { get; set; }

    protected virtual void Start()
    {
        _targetPlayer = FindAnyObjectByType<Player>();
    }

    protected virtual void Update()
    {
        CheckFlip();
    }

    public void CheckFlip() 
    {
        var s = transform.localScale;
        if(IsFacingRight && Direction.x < 0f) 
        {
            IsFacingRight = !IsFacingRight;
            s.x *= -1;
            transform.localScale = s;
        }
        else if(!IsFacingRight && Direction.x > 0f)
        {
            IsFacingRight = !IsFacingRight;
            s.x *= -1;
            transform.localScale = s;
        }
    }
   

    public Vector2 GetDirectionToPlayer() {
        var v = _targetPlayer.transform.position - transform.position;
        v.y = 0f;
        return v.normalized;
    }

    public virtual void Damage(int damage)
    {
        CurrentHealth -= damage;
        if(CurrentHealth <= 0) {
            Die();
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2(-transform.localScale.x * 2f, 2f);
    }
    
    public virtual void Die() 
    {
        Destroy(gameObject);
    }
}
