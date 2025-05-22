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

    [Header("Skyboxs Material")]
    [SerializeField] private Material windySkybox;
    [SerializeField] private Material blueSkybox;
    [SerializeField] private Material stormSkybox;
    [SerializeField] private Material rainSkybox;
    [SerializeField] private Material snowSkybox;

    [Header("Particles")]
    [SerializeField] private GameObject rainParticles;
    [SerializeField] private GameObject snowParticles;

    [Header("Patrol Points")]
    [SerializeField] private List<Transform> patrolPoints = new();

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
                    _stateMachine.AddState(States.Patrol, new PatrolState(this, _stateMachine, patrolPoints));
                    break;
                case States.Chase:
                    _stateMachine.AddState(States.Chase, new ChaseState(this, _stateMachine));
                    break;
                case States.Attack:
                    _stateMachine.AddState(States.Attack, new AttackState(this, _stateMachine));
                    break;

                case States.BlueSky:
                    _stateMachine.AddState(States.BlueSky, new BlueSky(this, _stateMachine, blueSkybox));
                    break;
                case States.Rain:
                    _stateMachine.AddState(States.Rain, new Rain(this, _stateMachine, rainSkybox, rainParticles));
                    break;
                case States.Snow:
                    _stateMachine.AddState(States.Snow, new Snow(this, _stateMachine, snowSkybox, snowParticles));
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
        Snow,

        Fleeing,
        Searching
    }