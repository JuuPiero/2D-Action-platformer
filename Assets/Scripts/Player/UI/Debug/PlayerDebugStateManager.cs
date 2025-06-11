using System;
using TMPro;
using UnityEngine;
public class PlayerDebugStateManager : MonoBehaviour {
    [SerializeField] private Player _player;
    [SerializeField] private TextMeshPro _stateText;
    [SerializeField] private TextMeshPro _prevStateText;


    void Start()
    {
        _player = GetComponentInParent<Player>();
        _player.StateMachine.OnStateChange += ChangeTextState;
    }

    void ChangeTextState()
    {
        _stateText.text = _player.StateMachine.CurrentState.GetType().Name.Replace("Player", "").Replace("State", "");
        _prevStateText.text = "Prev: " + _player.StateMachine.PrevState?.GetType().Name.Replace("Player", "").Replace("State", "");
    }
}
