using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    State _currentState = null;

    Dictionary<AgentStates, State> _allStates = new Dictionary<AgentStates, State>();

    public void Update()
    {
        _currentState?.OnUpdate();  // si _currentState existe, Hace el OnUpdate
    }

    public void AddState(AgentStates name, State state)
    {
        if (!_allStates.ContainsKey(name))  // si no tiene la Key darsela con el State
            _allStates.Add(name, state);
        else
            _allStates[name] = state; // si la tiene cambia el State a esa Key

        state.fsm = this;  //esto le da la referencia de este FSM a cada State
    }

    public void ChangeState(AgentStates state)
    {
        _currentState?.OnExit();

        if (_allStates.ContainsKey(state))
            _currentState = _allStates[state];

        _currentState.OnEnter();
    }

}
