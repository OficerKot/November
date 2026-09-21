using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour 
{
    PlayerInput input;
    PlayerMovement movement;
    PlayerMovementState movementState;
    PlayerCrouching crouchingComp;
    PlayerEnviropmentDetector enviropmentDetector;

    SpeedCalculator speedCalculator;
    TemporarySpeedModifier temporarySpeedModifier;

    [SerializeField] PlayerConfig config;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        movement = GetComponent<PlayerMovement>();
        crouchingComp = GetComponent<PlayerCrouching>();
        enviropmentDetector = GetComponent<PlayerEnviropmentDetector>();

        movementState = new PlayerMovementState();
        temporarySpeedModifier = new TemporarySpeedModifier();
        speedCalculator = new SpeedCalculator(temporarySpeedModifier, config);
    }
    private void FixedUpdate()
    {
        temporarySpeedModifier.Tick(Time.fixedDeltaTime);
        UpdateMovementState();
        MovePlayer(speedCalculator.CalculateSpeed(
            movement.GetVelocity(),
            GetTargetDirection(input.moveAxis),
            movementState));
    }

    void MovePlayer(float speed)
    {
        movement.Move(input.moveAxis, speed);
        movement.Rotate(input.lookAxis, config.MouseSensitivity);

        if (movementState.isJumping)
        {
            movement.Jump(config.JumpVelocity, config.JumpCooldown);
        }
    }

    void UpdateMovementState()
    {
        movementState.isJumping = (input.IsJumpHeld && enviropmentDetector.CheckGround());

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

        if (input.CheckRun())
        {
            movementState.isRunning = !movementState.isRunning;
        }

        if (input.moveAxis.y <= 0 || movementState.isCroucning)
        {
            movementState.isRunning = false;
        }
    }

    public Vector3 GetTargetDirection(Vector2 input)
    {
        Vector3 direction =
            transform.right * input.x +
            transform.forward * input.y;

        return direction.normalized;
    }
}
