using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimation : MonoBehaviour
{
    public event Action OnAnimationTrigger;
    private Animator _anim;
    void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    void AnimationTrigger() {
        OnAnimationTrigger?.Invoke();
    }
}
