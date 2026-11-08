using System;
using System.Collections.Generic;
using Movement.States;
using UnityEngine;

public class PlayerMovementStateMachine : IStateSwitcher
{
    private IState _curMovementState;
    
    private Dictionary<Type, IState> _states = new Dictionary<Type, IState>();

    public PlayerMovementStateMachine(
        DefaultState defaultState,
        CrouchingState crouchingState,
        HangingState hangingState)
    {
        _states[typeof(DefaultState)] = defaultState;
        _states[typeof(CrouchingState)] = crouchingState;
        _states[typeof(HangingState)] = hangingState;
        
        SwitchState(typeof(DefaultState));
    }
    public void Tick(float deltaTime)
    {
        Type requestedState = _curMovementState.Tick(deltaTime);
        if (requestedState != null)
        {
            SwitchState(requestedState);
        }
    }
    public void SwitchState(Type stateType)
    {
        if (!_states.TryGetValue(stateType, out IState newState))
        {
            throw new InvalidOperationException(
                $"State {stateType.Name} is not registered.");
        }

        Debug.Log("Switching to " + stateType.Name);
        
        _curMovementState?.Exit();
        _curMovementState = newState;
        _curMovementState.Enter();
    }
}

public interface IStateSwitcher
{
    public void SwitchState(Type state);
}
