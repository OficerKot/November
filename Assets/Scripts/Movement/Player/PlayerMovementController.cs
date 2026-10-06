using UnityEngine;
using VContainer;

public class PlayerMovementController : MonoBehaviour
{
    [Inject] private PlayerMovementStateMachine stateMachine;

    private void FixedUpdate()
    {
        stateMachine.Tick(Time.fixedDeltaTime);
    }
}