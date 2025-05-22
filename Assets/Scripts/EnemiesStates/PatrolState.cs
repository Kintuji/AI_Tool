using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;
using System.Collections.Generic;
using System;


public class PatrolState : AI_State
{
    private List<Transform> _patrolPoints;
    private Transform currentPoint;
    public PatrolState(AI_Controller ai, AI_StateMachine<States> sm, List<Transform> patrolPoints) : base(ai, sm) 
    {
        _patrolPoints = patrolPoints;
    }

    public override void Enter()
    {
        Debug.Log("Patrol: Enter");
        target = _ai._aiData.Get<Transform>("Target");
        currentPoint = _patrolPoints[0];
    }
    public override void Tick()
    {
        Move();
    }

    public override void Exit()
    {
        Debug.Log("Patrol: Exit");
    }

    private void Move()
    {
        if (_patrolPoints == null || _patrolPoints.Count == 0) return;
        if (currentPoint == null) currentPoint = _patrolPoints[0];

        float distance = Vector3.Distance(currentPoint.position, _ai.transform.position);
        _ai.GetComponent<NavMeshAgent>().SetDestination(currentPoint.position);

        if (distance < 1f)
        {
            int index = _patrolPoints.IndexOf(currentPoint);

            if (index == -1) index = 0;
            index = (index + 1) % _patrolPoints.Count;

            currentPoint = _patrolPoints[index];
            _ai.GetComponent<NavMeshAgent>().SetDestination(currentPoint.position);
        }
    }
}

