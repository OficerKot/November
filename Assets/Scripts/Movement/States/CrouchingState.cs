using System;
using Movement.States;
using UnityEngine;

/// <summary>
/// Состояние приседа. Игрок перемещается более медленно и не имеет возможности активировать бег.
/// </summary>
public class CrouchingState : IState
{
    private readonly IHeadBlockDetector headBlockDetector;
    private readonly PlayerCrouching crouching;
    private readonly PlayerInput pInput;
    private readonly PlayerMovement movement;
    private readonly SpeedCalculator speedCalculator;
    private readonly PlayerMovementConfig config;
    public CrouchingState(
        IHeadBlockDetector headBlockDetector,
        PlayerCrouching crouching,
        PlayerInput pInput,
        PlayerMovement movement,
        SpeedCalculator speedCalculator,
        PlayerMovementConfig config)
    {
        this.headBlockDetector = headBlockDetector;
        this.crouching = crouching;
        this.pInput = pInput;
        this.movement = movement;
        this.speedCalculator = speedCalculator;
        this.config = config;
    }
    
    public Type Tick(float deltaTime)
    {
        if (pInput.CheckCrouch() && !headBlockDetector.CheckHeadBlock())
        {
            return typeof(DefaultState);
        }
        
        CheckJump();
        Move();
        
        movement.Tick(deltaTime);
        speedCalculator.Tick(deltaTime);
        return null;
    }

    public void Enter()
    {
        crouching.Toggle(config.CrouchPlayerHeight, 0.5f);
    }

    public void Exit()
    {
        crouching.Toggle(config.DefaultPlayerHeight, 0);
    }

    private void CheckJump()
    {
        if (pInput.IsJumpHeld && movement.IsGrounded)
        {
            movement.Jump(config.JumpForce, config.JumpCooldown);
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
            movement.transform.right * input.x +
            movement.transform.forward * input.y;

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
