
using UnityEngine;

public class SkeletonWalkState : SkeletonState
{
    public SkeletonWalkState(string animationName, Skeleton skeleton) : base(animationName, skeleton)
    {}


    public override bool IsMatchingConditions()
    {
        return _skeleton.canChase;
    }

    public override void Update() {
        base.Update();
        if (AudioManager.Instance != null && !AudioManager.Instance.sfxSource.isPlaying) {
            AudioManager.Instance?.PlaySFX("SkeletonWalk");
        }
    }

    public override void Exit() {
        base.Exit();
        AudioManager.Instance?.sfxSource.Stop();
    }
}