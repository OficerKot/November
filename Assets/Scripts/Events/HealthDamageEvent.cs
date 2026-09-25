using UnityEngine;

namespace Events
{
    public readonly struct HealthDamageEvent
    {
        private readonly float damage;

        public HealthDamageEvent(float damage)
        {
            this.damage = damage;
        }
    }
}