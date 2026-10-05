using Movement.States;
using UnityEngine;

public class DefaultState : IState
{   
    private readonly PlayerMovementConfig config;
    private readonly PlayerInput input;
    private readonly PlayerMovement movement;
    private readonly PlayerMovementState movementState;
    private readonly Transform playerTransform;
    private readonly SpeedCalculator speedCalculator;

    public DefaultState(
        PlayerMovementConfig config,
        PlayerInput input,
        PlayerMovement movement,
        PlayerMovementState movementState,
        SpeedCalculator speedCalculator,
        Transform playerTransform)
    {
        this.config = config;
        this.input = input;
        this.movement = movement;
        this.movementState = movementState;
        this.speedCalculator = speedCalculator;
        this.playerTransform = playerTransform;
    }
    
    public void Tick(float deltaTime)
    {
        Move();
        if (movementState.isJumping)
        {
            movement.Jump(config.JumpVelocity, config.JumpCooldown);
        }
        
        movement.Tick(deltaTime);
        speedCalculator.Tick(deltaTime);
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
        movementState.isRunning = false;
        movementState.isJumping = false;
    }
    private void Move()
    {
        Vector3 targetDirection = GetTargetDirection(input.moveAxis);
    
        float speed = speedCalculator.CalculateSpeed(
            movement.GetVelocity(),
            targetDirection,
            movementState);
    
        Vector3 velocity = targetDirection* speed;
        movement.Move(velocity);
    }
    
    public Vector3 GetTargetDirection(Vector2 input)
    {
        Vector3 direction =
            playerTransform.right * input.x +
            playerTransform.forward * input.y;

        return direction.normalized;
    }
}
