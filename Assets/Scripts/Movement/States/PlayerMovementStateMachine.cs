using Movement.States;
using UnityEngine;

public class PlayerMovementStateMachine
{
    private IState _curMovementState;
    
    public void SetMovementState(IState state)
    {
        if (_curMovementState != null)
        {
            _curMovementState.Exit();
        }
        _curMovementState = state;
        _curMovementState.Enter();
    }
}
