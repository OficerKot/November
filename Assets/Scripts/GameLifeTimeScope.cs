using Events;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifeTimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<EventBus>(Lifetime.Singleton)
            .As<IEventBus>()
            .As<IReadOnlyEventBus>();
        
        builder.RegisterComponentInHierarchy<PlayerStatsController>();
        builder.RegisterComponentInHierarchy<HealthView>();
    }
}
