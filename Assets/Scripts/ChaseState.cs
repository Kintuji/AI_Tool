using UnityEngine;

public class ChaseState : AI_State
{
    public ChaseState(AI_Controller ai, AI_StateMachine<States> stateMachine) : base(ai, stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Is chasing");
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }

    public override void Tick()
    {
        throw new System.NotImplementedException();
    }
}
