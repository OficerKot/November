using UnityEngine;
public class PlayerMovementController : MonoBehaviour 
{
    PlayerMovementStateMachine stateMachine = new ();
    
    private void FixedUpdate()
    {
        stateMachine.Tick(Time.fixedDeltaTime);
    }
}
