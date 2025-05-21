using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.AI;
using System;

public class ChaseState : AI_State
{

    private Action _chaseTarget;

    public Action ChaseTarget { get => _chaseTarget; set => _chaseTarget = value; }

    public ChaseState(AI_Controller ai, AI_StateMachine<States> stateMachine) : base(ai, stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Is chasing");
        target = _ai._aiData.Get<Transform>("Target");
       
    }
    public override void Tick()
    {
        _distanceToPlayer = Vector3.Distance(_ai.transform.position, target.position);

        if (_distanceToPlayer > 10 || !_ai._aiData.Has("Target"))
        {
            _stateMachine.ChangeState(States.Idle);
        }
        if (_distanceToPlayer < 3.5f)
        {
            _stateMachine.ChangeState(States.Attack);
        }
        else
        {
            _ai.GetComponent<NavMeshAgent>().SetDestination(target.position);
        }
    }
    public override void Exit()
    {
        
    }
}
