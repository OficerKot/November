using UnityEngine;

namespace Hands
{
    public class Stamina
    {
        float defaultStamina;
        float curStamina;

        public float CurStamina => curStamina;

        public Stamina(float defaultStamina)
        {
            this.defaultStamina = defaultStamina;
            curStamina = defaultStamina;
        }
        public void Drain(float val, float deltaTime)
        {
            curStamina -= val * deltaTime;
            curStamina = Mathf.Max(curStamina, 0f);
        }

        public void Recover(float val, float deltaTime)
        {
            curStamina += val * deltaTime;
            curStamina = Mathf.Min(curStamina, defaultStamina);
        }
    }
}