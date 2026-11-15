using Movement.States;
using System;
using UnityEngine;
using UnityEngine.Windows;

/// <summary>
/// Состояние приседа. Игрок перемещается более медленно и не имеет возможности активировать бег.
/// </summary>
public class CrouchingState : IState
{
    private readonly IHeadBlockDetector headBlockDetector;
    private readonly PlayerCrouching crouching;
    private readonly PlayerInput input;
    private readonly PlayerMovement movement;
    private readonly SpeedCalculator speedCalculator;
    private readonly PlayerMovementConfig config;
    private readonly PlayerCameraController cameraController;
    public CrouchingState(
        IHeadBlockDetector headBlockDetector,
        PlayerCrouching crouching,
        PlayerInput input,
        PlayerMovement movement,
        SpeedCalculator speedCalculator,
        PlayerMovementConfig config,
        PlayerCameraController cameraController
        )
    {
        this.headBlockDetector = headBlockDetector;
        this.crouching = crouching;
        this.input = input;
        this.movement = movement;
        this.speedCalculator = speedCalculator;
        this.config = config;
        this.cameraController = cameraController;
    }
    
    public Type Tick(float deltaTime)
    {
        cameraController.LookAround(
            input.lookAxis,
            movement.transform);

        if (input.CheckCrouch() && !headBlockDetector.CheckHeadBlock())
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
        if (input.IsJumpHeld && movement.IsGrounded)
        {
            movement.Jump(config.CrouchJumpForce, config.JumpCooldown);
        }
    }
    private void Move()
    {
        Vector3 targetDirection = GetTargetDirection(input.moveAxis);

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
            brakeAcceleration: config.WalkBreakAcceleration,
            minSpeed: config.WalkMinSpeed);
    }
    
}
