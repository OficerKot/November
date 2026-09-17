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
    }
    private void FixedUpdate()
    {
        movement.Move(input.moveAxis, CountCurSpeed());
        movement.Rotate(input.lookAxis, config.mouseSensitivity);
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
