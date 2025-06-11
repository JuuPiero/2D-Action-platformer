using System;
using UnityEngine;

public class PlayerResource : MonoBehaviour
{
    public event Action OnPlayerResourceChanged;

    [SerializeField] private float _currentHealth;
    public float CurrentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = value;
            OnPlayerResourceChanged?.Invoke();
        }
    }
    [SerializeField] private float _currentMana;
    public float CurrentMana
    {
        get => _currentMana;
        set
        {
            _currentMana = value;
            OnPlayerResourceChanged?.Invoke();
        }
    }

    [SerializeField] private float _currentStamina;
    public float CurrentStamina {
        get => _currentStamina;
        set
        {
            _currentStamina = value;
            OnPlayerResourceChanged?.Invoke();
        }
    }

    public void Init(float health, float mana, float stamina)
    {
        CurrentHealth = health;
        CurrentMana = mana;
        CurrentStamina = stamina;
    }
  
}