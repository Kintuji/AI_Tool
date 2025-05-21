using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;

public abstract class AI_State
{
    protected AI_Controller _ai;
    protected AI_StateMachine<States> _stateMachine;

    protected float _distanceToPlayer;
    protected Transform target;

    public AI_State(AI_Controller ai, AI_StateMachine<States> stateMachine)
    {
        _ai = ai;
        _stateMachine = stateMachine;
    }

    public abstract void Enter();
    public abstract void Tick();
    public abstract void Exit();
}
