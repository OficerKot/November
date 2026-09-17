using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour 
{
    PlayerInput input;
    PlayerMovement movement;
    PlayerMovementState movementState;
    PlayerCrouching crouchingComp;
    GroundDetector groundDetector;

    [SerializeField] PlayerConfig config;


    private void Update()
    {
        
        if (input.CheckJump() && groundDetector.IsGrounded)
        {
            movement.Jump(config.JumpForce);
        }

        if (input.CheckRun())
        {
            movementState.isRunning = !movementState.isRunning;
        }

        if (input.CheckCrouch())
        {
            if (movementState.isCroucning)
            {
                crouchingComp.Toggle(config.DefaultColliderHeight, config.DefaultCameraHeight);
            }
            else
            {
                crouchingComp.Toggle(config.CrouchColliderHeight, config.CrouchCameraHeight);
            }
            movementState.isCroucning = !movementState.isCroucning;
            
        }
       
    }
    private void FixedUpdate()
    {
        movement.Move(input.moveAxis, CountCurSpeed());
        movement.Rotate(input.lookAxis, config.MouseSensitivity);

        if(input.moveAxis == Vector2.zero || input.moveAxis.y < 0)
        {
            movementState.isRunning = false;
        }
    }

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        movement = GetComponent<PlayerMovement>();
        crouchingComp = GetComponent<PlayerCrouching>();
        groundDetector = GetComponent<GroundDetector>();
        movementState = new PlayerMovementState();
    }

    float CountCurSpeed()
    {
        float speed = config.WalkSpeed;
        if (movementState.isRunning)
        {
            speed *= config.RunSpeedMultiplier;
        }
        if (movementState.isCroucning)
        {
            speed *= config.CrouchSpeedMultiplier;
        }

        return speed;
    }

}
