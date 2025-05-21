using System.Collections.Generic;
using NUnit.Framework;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Analytics.IAnalytic;

public class AI_Controller : MonoBehaviour
{
    public AI_StateMachine<States> _stateMachine { get; private set; }
    public AI_Data _aiData { get; private set; }

    [Header("AI States Setup")]
    [SerializeField] private List<States> activeStates = new();
    [SerializeField] private States startState;

    private Transform _target;
    private Rigidbody _rb;

    private void Awake()
    {
        _aiData = new AI_Data();
        _stateMachine = new AI_StateMachine<States>(this);
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _aiData.Set("Target", _target);
        _aiData.Set("Rigidbody", _rb);

        foreach (var state in activeStates)
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
        if (activeStates.Contains(startState))
        {
            _stateMachine.ChangeState(startState);
        }
        else if (activeStates.Count > 0)
        {
            _stateMachine.ChangeState(activeStates[0]);
        }
        //foreach (var pair in _stateMachine.States)
        //{
        //    Debug.Log($"[StateMachine] Key: {pair.Key}, State: {pair.Value.GetType().Name}");
        //}
 

    }
    private void Update()
    {
        _stateMachine?.Tick();
    }
}
    public enum States
    {
        Idle,
        Patrol,
        Chase,
        Attack,

        Windy,
        BlueSky,
        Storm,
        Rain,
        snowing,

        Fleeing,
        Searching
    }