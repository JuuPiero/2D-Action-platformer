
using UnityEngine;

public class SkeletonWalkState : SkeletonState
{
    public SkeletonWalkState(string animationName, Skeleton skeleton) : base(animationName, skeleton)
    {}


    public override bool IsMatchingConditions()
    {
        return _skeleton.canChase;
    }
}