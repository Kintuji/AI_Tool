using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Snow : AI_State
{
    public Snow(AI_Controller ai, AI_StateMachine<States> stateMachine, Material skybox, GameObject particles) : base(ai, stateMachine)
    {
        _skyBoxMaterial = skybox;
        _particles = particles;
    }

    public override void Enter()
    {
        Debug.Log("Snow State Entered");
        RenderSettings.skybox = _skyBoxMaterial;
        _particles.SetActive(true);
    }
    public override void Tick()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _particles.SetActive(false);
            _stateMachine.ChangeState(States.Rain);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _particles.SetActive(false);
            _stateMachine.ChangeState(States.BlueSky);
        }
    }

    public override void Exit()
    {

    }

}
