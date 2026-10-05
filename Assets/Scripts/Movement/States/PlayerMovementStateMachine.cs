using Movement.States;
using UnityEngine;

public class PlayerMovementStateMachine : IStateSwitcher
{
    private IState _curMovementState;
    public IState CurMovementState => _curMovementState;
    
    public void Tick(float deltaTime)
    {
        _curMovementState.Tick(deltaTime);
    }
    public void SwitchState(IState state)
    {
        if (_curMovementState != null)
        {
            _curMovementState.Exit();
        }
        _curMovementState = state;
        _curMovementState.Enter();
    }
}

public interface IStateSwitcher
{
    public void SwitchState(IState state);
}
