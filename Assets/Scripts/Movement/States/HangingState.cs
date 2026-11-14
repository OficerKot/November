using System;
using Climbing;
using Events;
using Movement;
using Movement.States;
using UnityEngine;
using VContainer;

/// <summary>
/// —осто€ние виса на зацепе. —тандартное управление замен€етс€ на скалолазание.
/// </summary>
public class HangingState : IState
{
    private HangingMovement hangingMovement;
    private ClimbConfig config;
    private PlayerInput input;
    private PlayerCameraController cameraController;

    public HangingState(
        HangingMovement hangingMovement,
        ClimbConfig config,
        PlayerInput input,
        PlayerCameraController cameraController)
    {
        this.hangingMovement = hangingMovement;
        this.config = config;
        this.input = input;
        this.cameraController = cameraController;
    }
    public Type Tick(float deltaTime)
    {
        CheckJump();
        Swing();
        cameraController.LookAround(
            input.lookAxis,
            hangingMovement.transform);
        return null;
    }

    public void Enter()
    {
        cameraController.SetHangingStrategy();
    }

    public void Exit()
    {
        // камеру и тело повернуть в одном направлении
    }

    private void CheckJump()
    {
        if (input.IsJumpHeld)
        {
            hangingMovement.Jump(config.JumpForce, config.JumpCooldown);
        }
        //руки отцепитьс€ должны
    }

    private void Swing()
    {
        hangingMovement.Move(Vector3.zero);
    }
}
