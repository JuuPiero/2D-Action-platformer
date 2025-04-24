
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.IO.LowLevel.Unsafe;

[Serializable]
public class StateMachine  {
    protected List<State> states = new List<State>();

    //protected Dictionary<string, State> statesByState = new Dictionary<string, State>();

    public State CurrentState { get; set; }
    public State PrevState { get; set; }

    public void Initialize(State state = null) {
        CurrentState = state ?? states.First();
        PrevState = CurrentState;
        CurrentState.Enter();
    }

    public void ChangeState(State newState)
    {
        if(CurrentState == newState) return;
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }

    // public void ChangeState(string stateName) {
    //     if(CurrentState == statesByState[stateName]) return;
    //     CurrentState?.Exit();
    //     CurrentState = statesByState[stateName];
    //     CurrentState.Enter();
    // }

  
    public void AddState(State state, int priority = 0)
    {
        state.StateMachine ??= this;
        // states.Add(state);
        states.Insert(0, state);
        //Debug
        //statesByState.Add(state.AnimationName, state);
    }

    public void Update() 
    {
        if (CurrentState.CanExit)  // Chỉ tìm state mới nếu state hiện tại cho phép
        {
            foreach (State state in states) {
                if (state.IsMatchingConditions()) {
                    ChangeState(state);
                    break;
                }
            }
        }
        CurrentState.Update(); // Tiếp tục chạy logic của state hiện tại
    }

    public void FixedUpdate() {
        CurrentState?.FixedUpdate();
    }

    public bool CurrentIs<T>() where T : State
    {
       return CurrentState.GetType() == typeof(T);
    }
    public State GetState<T>() where T : State
    {
        var state = states.Where(s => s.GetType() == typeof(T)).FirstOrDefault();
        return state != null ? state : null;
    }

}