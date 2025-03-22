using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] public Animator Anim { get; private set; }
    
    public event Action OnAttack;
    public event Action OnAnimationTrigger;

    void Start()
    {
        _player = GetComponentInParent<Player>(); 
        Anim = GetComponent<Animator>();      
    }

    public void Attack() 
    {
        OnAttack?.Invoke();
    }

    public void AnimatonTrigger() 
    {
        OnAnimationTrigger?.Invoke();
    }

    
}
