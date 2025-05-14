using System.Collections.Generic;
using NUnit.Framework;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.Analytics.IAnalytic;

public class AI_Controller : MonoBehaviour
{
    public AI_StateMachine<States> _stateMachine { get; private set; }
    public AI_Data _aiData { get; private set; }

    [Header("AI States Setup")]
    [SerializeField] private List<States> _activeStates = new();
    [SerializeField] private States _startState;

    [SerializeField] private List<int> _changeState = new();

    private void Awake()
    {
        _aiData = new AI_Data();
        _stateMachine = new AI_StateMachine<States>(this);

        foreach (var state in _activeStates)
        {
            switch (state)
            {
                case States.Idle:
                    _stateMachine.AddState(States.Idle, new IdleState(this, _stateMachine));
                    break;
                case States.Patrol:
                    _stateMachine.AddState(States.Patrol, new PatrolState(this, _stateMachine));
                    break;
                case States.Chase:
                    _stateMachine.AddState(States.Chase, new ChaseState(this, _stateMachine));
                    break;
                case States.Attack:
                    _stateMachine.AddState(States.Attack, new AttackState(this, _stateMachine));
                    break;
            }
        }
    }
    private void Start()
    {
        if (_activeStates.Count == 0)
        {
            Debug.LogWarning($"AIController in '{gameObject.name}' has no active states assigned.");
            return;
        }

        if (_activeStates.Contains(_startState))
        {
            _stateMachine.ChangeState(_startState);
        }
        else
        {
            Debug.LogWarning($"StartState '{_startState}' not in activeStates. Using first one.");
            _stateMachine.ChangeState(_activeStates[0]);
        }
    }

    private void Update()
    {
        //foreach (var events in _changeState)
        //{
        //    switch (events)
        //    {
        //        case 1:_stateMachine.ChangeState(States.Patrol);
        //            break;
        //    }
        //}
    }
}
    public enum States
{
    Idle,
    Patrol,
    Chase,
    Attack,

    Windy,
    Blue,
    Storm,
    Rain,

    Fleeing,
    Searching
}
