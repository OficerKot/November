using System;
using Movement.States;
using UnityEngine;

/// <summary>
/// Стандартное состояние передвижения игрока (бег/ходьба). 
/// </summary>
public class DefaultState : IState
{   
    private readonly PlayerMovementConfig config;
    private readonly PlayerInput input;
    
    private readonly PlayerMovement movement;
    private readonly SpeedCalculator speedCalculator;
    
    private bool isRunning;
    private bool isJumping;

    public DefaultState(
        PlayerMovementConfig config,
        PlayerInput input,
        PlayerMovement movement,
        SpeedCalculator speedCalculator)
    {
        this.config = config;
        this.input = input;
        this.movement = movement;
        this.speedCalculator = speedCalculator; 
    }
    
    public Type Tick(float deltaTime)
    {
        if (input.CheckCrouch())
        {
            return typeof(CrouchingState);
        }
        
        CheckMovements();
        Move();
        if(isJumping)
        {
            movement.Jump(config.JumpForce, config.JumpCooldown);
        }
        
        movement.Tick(deltaTime);
        speedCalculator.Tick(deltaTime);
        
        return null;
    }

    public void Enter()
    { 
        ClearStates();
    }

    public void Exit()
    {
       ClearStates();
    }

    private void ClearStates()
    {
        isRunning = false;
        isJumping = false;
    }
    private void Move()
    {
        Vector3 targetDirection = GetTargetDirection(input.moveAxis);
    
        float speed = speedCalculator.CalculateSpeed(
            movement.GetVelocity(),
            targetDirection,
            GetSpeedParameters()
            );
    
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

    private void CheckMovements()
    {
        isJumping = (input.IsJumpHeld && movement.IsGrounded);
        CheckRun();
    }

    private void CheckRun()
    {
        if (input.moveAxis.y <= 0)
        {
            isRunning = false;
            return;
        }
        if (input.CheckRun()) isRunning = !isRunning;
    }
    private SpeedCalculator.MovementSpeedParameters GetSpeedParameters()
    {
        float targetSpeed = isRunning? config.WalkSpeed * config.RunSpeedMultiplier : config.WalkSpeed;
        float acceleration = isRunning? config.RunAcceleration : config.WalkAcceleration;
        float brakeAcceleration = isRunning ? config.RunBreakAcceleration : config.WalkBreakAcceleration;
        
        return new SpeedCalculator.MovementSpeedParameters(
            targetSpeed: targetSpeed,
            acceleration: acceleration,
            brakeAcceleration: brakeAcceleration);
    }
}
