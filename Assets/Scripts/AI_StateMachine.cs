using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class AI_StateMachine<TState> where TState : System.Enum
{
    private readonly Dictionary<TState, AI_State> _states = new();
    private AI_State _currentState;
    private readonly AI_Controller _controller;

    public Dictionary<TState, AI_State> States => _states;

    public AI_StateMachine(AI_Controller controller) => _controller = controller;

    public void AddState(TState stateType, AI_State state)
    {
        if (!_states.ContainsKey(stateType))
            _states[stateType] = state;
    }

    public void ChangeState(TState stateType)
    {
        if (!_states.ContainsKey(stateType))
        {
            Debug.LogWarning($"State '{stateType}' not registered.");
            return;
        }

        _currentState?.Exit();
        _currentState = _states[stateType];
        _currentState?.Enter();
        _currentState?.Tick();
    }

    public void Tick()
    {
        _currentState?.Tick();
    }
}
