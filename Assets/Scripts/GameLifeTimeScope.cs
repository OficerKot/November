using Events;
using Movement.Player;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifeTimeScope : LifetimeScope
{
    [SerializeField] PlayerMovementConfig playerMovementConfig;
    [SerializeField] PlayerStatsConfig playerStatsConfig;
    [SerializeField] private CameraConfig cameraConfig;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<EventBus>(Lifetime.Singleton)
            .As<IEventBus>()
            .As<IReadOnlyEventBus>();

        builder.RegisterInstance(playerStatsConfig);
        builder.RegisterComponentInHierarchy<PlayerStatsController>();
        builder.RegisterComponentInHierarchy<HealthView>();

        builder.RegisterComponentInHierarchy<PlayerCamera>();
        builder.RegisterComponentInHierarchy<PlayerMovementController>();
        builder.RegisterInstance(cameraConfig);
        builder.RegisterInstance(playerMovementConfig);
    }
}
