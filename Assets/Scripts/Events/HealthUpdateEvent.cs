using UnityEngine;

namespace Events
{
    public readonly struct HealthUpdateEvent
    {
        public readonly float curHealth;
        public readonly float difference;
        public readonly bool isDamaged;

        public HealthUpdateEvent(float curHealth, float difference, bool isDamaged)
        {
            this.curHealth = curHealth;
            this.difference = difference;
            this.isDamaged = isDamaged;
        }
    }
}