using System;
using UnityEngine;

//тоже потом вынести 
public interface IDamageable
{
    public void TakeDamage(float damage);
}
public class PlayerStatsController : MonoBehaviour, IDamageable
{
    [SerializeField] PlayerStatsConfig config;
    PlayerStats stats;

    public event Action HealthEmpty;
    public event Action HealthFull;

    private void Awake()
    {
        stats = new();
        stats.SetHealth(config.MaxHealth);
    }
    public void TakeDamage(float damage)
    {
        if (stats.health - damage <= config.MinHealth)
        {
            stats.SetHealth(0f);
            HealthEmpty?.Invoke();
        }
        else
        {
            stats.SetHealth(stats.health - damage);
        }
    }

    public void Heal(float healValue)
    {
        if (stats.health + healValue >= config.MaxHealth)
        {
            stats.SetHealth(config.MaxHealth);
            HealthFull?.Invoke();
        }
        else
        {
            stats.SetHealth(stats.health + healValue);
        }
    }

    //Методы изменения веса игрока тоже будут тут
}
