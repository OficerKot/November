using System;
using System.Collections.Generic;
using Movement.States;

public class PlayerMovementStateMachine : IStateSwitcher
{
    private IState _curMovementState;
    
    private Dictionary<Type, IState> _states = new Dictionary<Type, IState>();

    public PlayerMovementStateMachine(
        DefaultState defaultState,
        CrouchingState crouchingState,
        HookingState hookingState)
    {
        _states[typeof(DefaultState)] = defaultState;
        _states[typeof(CrouchingState)] = crouchingState;
        _states[typeof(HookingState)] = hookingState;
        
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
        
        _curMovementState?.Exit();
        _curMovementState = newState;
        _curMovementState.Enter();
    }
}

public interface IStateSwitcher
{
    public void SwitchState(Type state);
}
