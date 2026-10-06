using System;
using Events;
using Movement.States;
using UnityEngine;

/// <summary>
/// Состояние виса на зацепе. Стандартное управление заменяется на паркур.
/// </summary>
public class HookingState : IState
{
    private PlayerMovement movement;
    private PlayerInput input;
    public Type Tick(float deltaTime)
    {
        throw new System.NotImplementedException();
    }

    public void Enter()
    {
        movement.ClearVelocity();
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }
    
}
