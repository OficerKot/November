namespace Movement.States
{
    public interface IState
    {
        public void Tick(float deltaTime);
        public void Enter();
        public void Exit();
    }
}