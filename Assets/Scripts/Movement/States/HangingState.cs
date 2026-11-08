using System;
using Climbing;
using Events;
using Movement;
using Movement.States;
using UnityEngine;
using VContainer;

/// <summary>
/// Состояние виса на зацепе. Стандартное управление заменяется на паркур.
/// </summary>
public class HangingState : IState
{
    private PlayerMovement defaultMovement;
    private HangingMovement hangingMovement;
    private ClimbConfig config;
    private PlayerInput input;

    public HangingState(
        PlayerMovement defaultMovement,
        HangingMovement hangingMovement,
        ClimbConfig config,
        PlayerInput input)
    {
        this.defaultMovement = defaultMovement;
        this.hangingMovement = hangingMovement;
        this.config = config;
        this.input = input;
    }
    public Type Tick(float deltaTime)
    {
        CheckJump();
        Swing();

        return null;
    }

    public void Enter()
    {
        defaultMovement.ClearVelocity();
    }

    public void Exit()
    {
        
    }

    private void CheckJump()
    {
        if (input.IsJumpHeld)
        {
            hangingMovement.Jump(config.JumpForce, config.JumpCooldown);
        }
        //руки отцепиться должны
    }

    private void Swing()
    {
        hangingMovement.Move(Vector3.zero);
    }
}
