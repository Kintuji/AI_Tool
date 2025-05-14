using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PatrolState : AI_State
{
    public PatrolState(AI_Controller ai, AI_StateMachine<States> sm) : base(ai, sm) { }

    public override void Enter()
    {
        Debug.Log("Patrol: Enter");
    }

    public override void Tick()
    {
        Debug.Log("Patrolling...");
    }

    public override void Exit()
    {
        Debug.Log("Patrol: Exit");
    }
}

