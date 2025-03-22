
public interface IDamageable
{
    int CurrentHealth { get; set; }

    void Damage(int damage);
    void Die();

}