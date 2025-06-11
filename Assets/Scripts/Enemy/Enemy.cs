using UnityEngine;
public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] public LayerMask playerLayer;
    [SerializeField] protected Player _targetPlayer;

    //[SerializeField] protected CinemachineImpulseSource impulseSource;
    public bool IsFacingRight { get; set; }


    public Transform attackPoint;

    [field: SerializeField] public float CurrentHealth { get; set; } = 100f;
    [field: SerializeField] public Vector2 Direction { get; set; }

    protected virtual void Start()
    {
        _targetPlayer = FindFirstObjectByType<Player>();
    }

    protected virtual void Update()
    {
        CheckFlip();
    }

    public void CheckFlip()
    {
        if (IsFacingRight && Direction.x < 0f)
        {
            Flip();
        }
        else if (!IsFacingRight && Direction.x > 0f)
        {
            Flip();
        }
    }

    public Vector2 GetDirectionToPlayer()
    {
        var v = _targetPlayer.transform.position - transform.position;
        v.y = 0f;
        return v.normalized;
    }

    public virtual void Damage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Die();
        }
        //GetComponent<Rigidbody2D>().velocity = new Vector2(-transform.localScale.x * 2f, 2f);
    }

    public virtual void Die()
    {
        ItemManager.Instance?.SpawnItem(gameObject.transform.position);
        Destroy(gameObject);
    }
    
    public void Flip()
    {
        var s = transform.localScale;
        IsFacingRight = !IsFacingRight;
        s.x *= -1;
        transform.localScale = s;
    }
}
