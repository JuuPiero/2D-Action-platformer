
using System;
using System.Collections.Generic;

[Serializable]
public class StateMachine  {
    protected List<State> states = new List<State>();

    //protected Dictionary<string, State> statesByState = new Dictionary<string, State>();

    public State CurrentState { get; set; }
    public State PrevState { get; set; }

    public void Initialize() {
        CurrentState = states[0];
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

    public void AddState(State state)
    {
        state.StateMachine ??= this;
        states.Add(state);

        //Debug
        //statesByState.Add(state.AnimationName, state);
    }

    public void Update() 
    {
        if (CurrentState.CanExit)  // Chỉ tìm state mới nếu state hiện tại cho phép
        {
            foreach (State state in states) 
            {
                if (state.IsMatchingConditions())  
                {
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

    public bool CurrentIs<T>() where T : PlayerState
    {
       return CurrentState.GetType() == typeof(T);
    }
   

}