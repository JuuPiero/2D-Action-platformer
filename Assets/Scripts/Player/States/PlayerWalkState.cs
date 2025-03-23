
using UnityEngine;

public class PlayerWalkState : PlayerState 
{
    public PlayerWalkState(string animationName, Player player) : base(animationName, player)
    {
    }

    public override bool IsMatchingConditions()
    {
        return _player.IsGrounded && _player.InputHandler.Direction.x != 0f;
    }

    public override void Update()
    {
        base.Update();
        // if(!AudioManager.Instance.sfxSource.isPlaying) {
        //     return;
        // }
        // else {
        //     AudioManager.Instance?.PlaySFX("PlayerWalk");
        // }
        if (AudioManager.Instance != null && !AudioManager.Instance.sfxSource.isPlaying) {
            AudioManager.Instance?.PlaySFX("PlayerWalk");
        }

    }

    public override void Exit() {
        base.Exit();
        AudioManager.Instance?.sfxSource.Stop();
    }
}

public class PlayerSwordWalkState : PlayerWalkState 
{
    public PlayerSwordWalkState(string animationName, Player player) : base(animationName, player) {}

    public override bool IsMatchingConditions()
    {
        return base.IsMatchingConditions() && _player.isAttacking;
    }
}