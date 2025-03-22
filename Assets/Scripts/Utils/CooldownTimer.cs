using System;
using UnityEngine;

[Serializable]
public struct CooldownTimer
{
    public float time;

    public bool IsReady => time <= 0f; 

    public void Start(float duration) => time = duration;

    public void Update(float deltaTime) => time = Mathf.Max(0f, time - deltaTime);
}