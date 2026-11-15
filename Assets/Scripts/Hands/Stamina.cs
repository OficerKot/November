using UnityEngine;

namespace Hands
{
    /// <summary>
    /// Модель выносливости с диапазоном значений
    /// от 0 до начального значения.
    /// Поддерживает постепенное расходование и восстановление выносливости.
    /// Управляется извне.
    /// </summary>
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

        /// <summary>
        /// Постепенно уменьшает выносливость.
        /// </summary>
        /// <param name="amountPerSecond">Количество расходуемой выносливости в секунду.</param>
        /// <param name="deltaTime">Время, прошедшее с предыдущего обновления.</param>
        public void Drain(float amountPerSecond, float deltaTime)
        {
            curStamina -= amountPerSecond * deltaTime;
            curStamina = Mathf.Max(curStamina, 0f);
        }

        /// <summary>
        /// Повышение выносливости на заданную величину.
        /// </summary>
        /// <param name="amountPerSecond">Количество восстанавливаемой выносливости в секунду.</param>
        /// <param name="deltaTime">Время, прошедшее с предыдущего обновления.</param>
        public void Recover(float amountPerSecond, float deltaTime)
        {
            curStamina += amountPerSecond * deltaTime;
            curStamina = Mathf.Min(curStamina, defaultStamina);
        }
    }
}