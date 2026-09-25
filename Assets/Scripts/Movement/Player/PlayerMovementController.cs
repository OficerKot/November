using UnityEngine;

public class PlayerMovementController : MonoBehaviour 
{
    PlayerInput input;
    PlayerMovement movement;
    PlayerMovementState movementState;
    PlayerCrouching crouchingComp;
    PlayerEnviropmentDetector enviropmentDetector;

    SpeedCalculator speedCalculator;
    TemporarySpeedModifier temporarySpeedModifier;

    [SerializeField] PlayerMovementConfig config;

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
        MovePlayer();
        movement.Rotate(input.lookAxis, config.MouseSensitivity);

        if (movementState.isJumping)
        {
            movement.Jump(config.JumpVelocity, config.JumpCooldown);
        }
        
        movement.Tick(Time.fixedDeltaTime);
    }

    void MovePlayer()
    {
        Vector3 targetDirection = GetTargetDirection(input.moveAxis);
    
        float speed = speedCalculator.CalculateSpeed(
            movement.GetVelocity(),
            targetDirection,
            movementState);
    
        Vector3 velocity = targetDirection* speed;
        
        // Debug.Log(
        //     "Velocity: " + velocity +
        //     ", target dir: " + targetDirection +
        //     ", speed: " + speed);
        //
        movement.Move(velocity);
    }

    void UpdateMovementState()
    {
        movementState.isJumping = (input.IsJumpHeld && movement.IsGrounded);

        if (input.CheckCrouch())
        {
            if (movementState.isCroucning && !enviropmentDetector.CheckHeadBlock())
            {
                crouchingComp.Toggle(config.DefaultPlayerHeight, 0);
                movementState.isCroucning = false;
            }
            else if (!movementState.isCroucning)
            {
                crouchingComp.Toggle(config.CrouchPlayerHeight, 0.5f);
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
