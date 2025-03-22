
using UnityEngine;

public class PlayerStunState : PlayerState 
{
    public PlayerStunState(string animationName, Player player) : base(animationName, player)
    {
    }

    public override bool IsMatchingConditions() {
        return _player.isStuning;
    }


    public override void Update() {
        base.Update();
        if(_player.stunTimer.IsReady) {
            Debug.Log("Hết");
        }
        _player.stunTimer.Update(Time.deltaTime);
    }

}