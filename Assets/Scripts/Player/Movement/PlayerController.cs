using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour 
{
    PlayerInput input;
    PlayerMovement movement;
    PlayerMovementState movementState;
   
    [SerializeField] PlayerConfig config;


    private void Update()
    {
        if (input.CheckJump())
        {
            movement.Jump(config.jumpForce);
        }

        if (input.CheckRun())
        {
            movementState.isRunning = !movementState.isRunning;
        }
       
    }
    private void FixedUpdate()
    {
        movement.Move(input.moveAxis, CountCurSpeed());
        movement.Rotate(input.lookAxis, config.mouseSensitivity);

        if(input.moveAxis == Vector2.zero || input.moveAxis.y < 0)
        {
            movementState.isRunning = false;
        }
    }

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        movement = GetComponent<PlayerMovement>();
        movementState = new PlayerMovementState();
    }

    float CountCurSpeed()
    {
        float speed = config.walkSpeed;
        if (movementState.isRunning)
        {
            speed *= config.runSpeedMultiplier;
        }
        if (movementState.isCroucning)
        {
            speed *= config.crouchSpeedMultiplier;
        }

        return speed;
    }
}
