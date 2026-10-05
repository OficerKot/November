using Events;
using UnityEngine;
using VContainer;

public class PlayerMovementController : MonoBehaviour 
{
    PlayerInput input;
    PlayerMovement movement;
    PlayerMovementState movementState;
    
    PlayerCrouching crouching;
    PlayerEnviropmentDetector enviropmentDetector;
    PlayerMovementStateMachine stateMachine;

    SpeedCalculator speedCalculator;

    [Inject] PlayerMovementConfig config;
    [Inject] IReadOnlyEventBus eventBus;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        movement = GetComponent<PlayerMovement>();
        crouching = GetComponent<PlayerCrouching>();
        enviropmentDetector = GetComponent<PlayerEnviropmentDetector>();

        movementState = new PlayerMovementState();
        speedCalculator = new SpeedCalculator(config);
        
        eventBus.Subscribe<OnClimbEvent>(OnClimbStateChanged);
    }

    void OnClimbStateChanged(OnClimbEvent climbEvent)
    {
        movementState.isMovementBlocked = climbEvent.isClimbing;
        if(movementState.isMovementBlocked)
        {
            movementState.ResetMovementFlags();
            movement.ClearVelocity();
        }
    }
    private void FixedUpdate()
    {
        UpdateMovementState(); 

        if (!movementState.isMovementBlocked)
        {
            MovePlayer();
            if (movementState.isJumping)
            {
                movement.Jump(config.JumpVelocity, config.JumpCooldown);
            }
            movement.Tick(Time.fixedDeltaTime);
        }
    }
    

    void MovePlayer()
    {
        Vector3 targetDirection = GetTargetDirection(input.moveAxis);
    
        float speed = speedCalculator.CalculateSpeed(
            movement.GetVelocity(),
            targetDirection,
            movementState);
    
        Vector3 velocity = targetDirection* speed;
        movement.Move(velocity);
    }

    void UpdateMovementState()
    {
        movementState.isJumping = (input.IsJumpHeld && movement.IsGrounded);

        if (input.CheckCrouch())
        {
            if (movementState.isCroucning && !enviropmentDetector.CheckHeadBlock())
            {
                crouching.Toggle(config.DefaultPlayerHeight, 0);
                movementState.isCroucning = false;
            }
            else if (!movementState.isCroucning)
            {
                crouching.Toggle(config.CrouchPlayerHeight, 0.5f);
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
