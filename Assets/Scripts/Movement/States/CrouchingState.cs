using Movement.States;
using UnityEngine;

/// <summary>
/// Состояние приседа. Игрок перемещается более медленно и не имеет возможности активировать бег.
/// </summary>
public class CrouchingState : IState
{
    private readonly IHeadBlockDetector headBlockDetector;
    private readonly IStateSwitcher stateSwitcher;
    private readonly Transform playerTransform;
    private readonly DefaultState defaultState;
    private readonly PlayerCrouching crouching;
    private readonly PlayerInput pInput;
    private readonly PlayerMovement movement;
    private readonly SpeedCalculator speedCalculator;
    private readonly PlayerMovementConfig config;
    public CrouchingState(
        IHeadBlockDetector headBlockDetector,
        IStateSwitcher stateSwitcher,
        Transform playerTransform,
        DefaultState defaultState,
        PlayerCrouching crouching,
        PlayerInput pInput,
        PlayerMovement movement,
        SpeedCalculator speedCalculator,
        PlayerMovementConfig config)
    {
        this.headBlockDetector = headBlockDetector;
        this.stateSwitcher = stateSwitcher;
        this.playerTransform = playerTransform;
        this.defaultState = defaultState;
        this.crouching = crouching;
        this.pInput = pInput;
        this.movement = movement;
        this.speedCalculator = speedCalculator;
        this.config = config;
    }
    
    public void Tick(float deltaTime)
    {
        UpdateStates();
        Move();
    }

    public void Enter()
    {
        crouching.Toggle(config.DefaultPlayerHeight, 0);
    }

    public void Exit()
    {
        crouching.Toggle(config.CrouchPlayerHeight, 0.5f);
    }

    private void UpdateStates()
    {
        if (pInput.CheckCrouch() && !headBlockDetector.CheckHeadBlock())
        { 
            stateSwitcher.SwitchState(defaultState);
        }
        if (pInput.IsJumpHeld && movement.IsGrounded)
        {
            movement.Jump(config.JumpVelocity, config.JumpCooldown);
        }
    }
    private void Move()
    {
        Vector3 targetDirection = GetTargetDirection(pInput.moveAxis);

        float speed = speedCalculator.CalculateSpeed(
            movement.GetVelocity(),
            targetDirection,
            GetSpeedParameters());
    
        Vector3 velocity = targetDirection* speed;
        movement.Move(velocity);
    }
    
    private Vector3 GetTargetDirection(Vector2 input)
    {
        Vector3 direction =
            playerTransform.right * input.x +
            playerTransform.forward * input.y;

        return direction.normalized;
    }
    
    private SpeedCalculator.MovementSpeedParameters GetSpeedParameters()
    {
        return new SpeedCalculator.MovementSpeedParameters(
            targetSpeed: (config.WalkSpeed * config.CrouchSpeedMultiplier),
            acceleration: config.WalkAcceleration,
            brakeAcceleration: config.WalkBreakAcceleration);
    }
    
}
