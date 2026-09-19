using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour 
{
    PlayerInput input;
    PlayerMovement movement;
    PlayerMovementState movementState;
    PlayerCrouching crouchingComp;
    PlayerEnviropmentDetector enviropmentDetector;

    [SerializeField] PlayerConfig config;


    private void Update()
    {
        if (input.CheckCrouch())
        {
            if (movementState.isCroucning && !enviropmentDetector.CheckHeadBlock())
            {
                crouchingComp.Toggle(config.DefaultColliderHeight, config.DefaultCameraHeight);
                movementState.isCroucning = false;
            }
            else if (!movementState.isCroucning)
            {
                crouchingComp.Toggle(config.CrouchColliderHeight, config.CrouchCameraHeight);
                movementState.isCroucning = true;
            }
        } 
    }

    private void FixedUpdate()
    {
        movementState.isJumping = (input.IsJumpHeld && enviropmentDetector.CheckGround());
    
        if (input.CheckRun())
        {
            movementState.isRunning = !movementState.isRunning;
        }

        movement.Move(input.moveAxis, CountCurSpeed());
        movement.Rotate(input.lookAxis, config.MouseSensitivity);

        if (movementState.isJumping)
        {
            movement.Jump(config.JumpVelocity, config.JumpCooldown);
        }

        if (input.moveAxis == Vector2.zero || input.moveAxis.y < 0)
        {
            movementState.isRunning = false;
        }
    }

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        movement = GetComponent<PlayerMovement>();
        crouchingComp = GetComponent<PlayerCrouching>();
        enviropmentDetector = GetComponent<PlayerEnviropmentDetector>();
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
