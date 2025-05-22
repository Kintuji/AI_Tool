using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.XR;
using static UnityEngine.GraphicsBuffer;

public class IdleState : AI_State
{
    private float _maxDistance = 7;
    private Transform _target;
    [SerializeField] private UnityEvent _foundPlayer;
    [SerializeField] private LayerMask _playerLayer;
    public IdleState(AI_Controller ai, AI_StateMachine<States> sm) : base(ai, sm) { }

    
    public override void Enter()
    {
        Debug.Log("Idle: Enter");
        _playerLayer.value = LayerMask.GetMask("Player");
        Rigidbody rb = _ai._aiData.Get<Rigidbody>("_rb");
      //  rb.linearVelocity = Vector3.zero;
    }

    public override void Tick()
    {
        Search();
    }

    public override void Exit()
    {
        Debug.Log("Idle: Exit");
    }
    private void OnDrawGizmos()
    {
        
    }

    void Search()
    {
        Collider[] hitColliders = Physics.OverlapSphere(_ai.transform.position, _maxDistance);
        bool foundPlayer = false;

        for (var i = 0; i < hitColliders.Length; i++)
        {
            Transform tempTarget = hitColliders[i].transform;
            Vector3 direction = tempTarget.position - _ai.transform.position;
            Debug.DrawRay(_ai.transform.position, direction, Color.red);

            Player player = tempTarget.GetComponent<Player>();

            if (player != null)
            {
                RaycastHit hit;
                if (Physics.Raycast(_ai.transform.position, direction, out hit, _maxDistance, _playerLayer))
                {
                    _target = player.transform;
                    foundPlayer = true;
                    _stateMachine.ChangeState(States.Chase);
                    break;
                }
            }
        }
        if (!foundPlayer)
        {
            _target = null;
        }
    }
}
