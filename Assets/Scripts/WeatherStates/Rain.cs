using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Rain : AI_State
{
    public Rain(AI_Controller ai, AI_StateMachine<States> stateMachine, Material skybox, GameObject particles) : base(ai, stateMachine)
    {
        _skyBoxMaterial = skybox;
        _particles = particles;
    }

    public override void Enter()
    {
        Debug.Log("Rain State Entered");
        RenderSettings.skybox = _skyBoxMaterial;
        _particles.SetActive(true);
    }
    public override void Tick()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _particles.SetActive(false);
            _stateMachine.ChangeState(States.BlueSky);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _particles.SetActive(false);
            _stateMachine.ChangeState(States.Snow);
        }
    }

    public override void Exit()
    {

    }

}
