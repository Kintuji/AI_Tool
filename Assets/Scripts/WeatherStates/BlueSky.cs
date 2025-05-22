using UnityEngine;

public class BlueSky : AI_State
{
    public BlueSky(AI_Controller ai, AI_StateMachine<States> stateMachine, Material skybox) : base(ai, stateMachine)
    {
        _skyBoxMaterial = skybox;   
    }

    public override void Enter()
    {
        Debug.Log("Blue Sky State Entered");
        RenderSettings.skybox = _skyBoxMaterial;
    }
    public override void Tick()
    {
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            _stateMachine.ChangeState(States.Rain);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _stateMachine.ChangeState(States.Snow);
        }
    }
    public override void Exit()
    {
        
    }
}
