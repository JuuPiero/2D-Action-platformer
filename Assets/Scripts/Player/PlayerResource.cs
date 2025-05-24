using System;

public class PlayerResource
{
    private float _currentHealth;
    public float CurrentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = value;
            OnPlayerResourceChanged?.Invoke();
        }
    }
    public float CurrentMana { get; set; }
    public float CurrentStamina { get; set; }
    public event Action OnPlayerResourceChanged;
    public PlayerResource(float health, float mana, float stamina)
    {
        CurrentHealth = health;
        CurrentMana = mana;
        CurrentStamina = stamina;
    }

    public void Changed()
    {
        OnPlayerResourceChanged?.Invoke();
    }
}