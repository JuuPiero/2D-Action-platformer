
using System.Collections;
using UnityEngine;

public class PlayerStunState : PlayerState 
{

    protected SpriteRenderer _sr;

    public PlayerStunState(string animationName, Player player) : base(animationName, player)
    {
        _sr = _player.GetComponentInChildren<SpriteRenderer>();
    }

    public override bool IsMatchingConditions() {
        return _player.isStuning;
    }

    public override void Enter() {
        AudioManager.Instance?.PlaySFX("PlayerDamaged");
        _player?.StartCoroutine(Stun());
        base.Enter();
    }

    IEnumerator Stun() {
        float stunTime = _player.Data.stunTime;
        float elapsed = 0f;
        bool isVisible = true;
        while (elapsed < stunTime) {
            isVisible = !isVisible;
            _sr.color = isVisible ? Color.white : new Color(1, 1, 1, 0.5f); // Nhấp nháy giữa trắng và trong suốt
            yield return new WaitForSeconds(0.1f); // Điều chỉnh tốc độ nhấp nháy
            elapsed += 0.1f;
        }
        _sr.color = Color.white; // Trả về màu ban đầu
        _player.isStuning = false;
    }
}