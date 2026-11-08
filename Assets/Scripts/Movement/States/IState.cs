using System;

namespace Movement.States
{
    public interface IState
    {
        public Type Tick(float deltaTime);
        public void Enter();
        public void Exit();
    }
}