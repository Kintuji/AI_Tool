using UnityEngine;

public class AttackState : AI_State
{
    public AttackState(AI_Controller ai, AI_StateMachine<States> stateMachine) : base(ai, stateMachine)
    {
    }

    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void Tick()
    {
        Debug.Log("Attacking...");
        target = _ai._aiData.Get<Transform>("Target");
        _distanceToPlayer = Vector3.Distance(_ai.transform.position, target.position);
        if (_distanceToPlayer > 5.5f)
        {
            _stateMachine.ChangeState(States.Chase);
        }
    }
}
