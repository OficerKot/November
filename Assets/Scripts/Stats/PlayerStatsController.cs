using System;
using UnityEngine;
using VContainer;

//???? ????? ??????? 
public interface IDamageable
{
    public void TakeDamage(float damage);
}
public class PlayerStatsController : MonoBehaviour, IDamageable
{
    [SerializeField] PlayerStatsConfig config;
    PlayerStats stats;

    [Inject] private IEventBus eventBus;

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
            eventBus.Publish(new Events.HealthDepletedEvent());
        }
        else
        {
            stats.SetHealth(stats.health - damage);
        }
        
        eventBus.Publish(new Events.HealthUpdateEvent(
                stats.health,
                damage,
                true));
    }

    public void Heal(float healValue)
    {
        if (stats.health + healValue >= config.MaxHealth)
        {
            stats.SetHealth(config.MaxHealth);
        }
        else
        {
            stats.SetHealth(stats.health + healValue);
        }
    }

    //?????? ????????? ???? ?????? ???? ????? ???
}
