namespace Events
{
    public readonly struct OnClimbEvent
    {
        public readonly bool isClimbing;

        public OnClimbEvent(bool isClimbing)
        {
            this.isClimbing = isClimbing;
        }
    }
}