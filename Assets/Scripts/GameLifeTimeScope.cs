using Climbing;
using Events;
using Movement.Player;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifeTimeScope : LifetimeScope
{
    [Header("Configuration")]
    [SerializeField] PlayerMovementConfig playerMovementConfig;
    [SerializeField] PlayerStatsConfig playerStatsConfig;
    [SerializeField] private CameraConfig cameraConfig;
    [SerializeField] private ClimbConfig climbConfig;
    
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
        
        builder.RegisterInstance(climbConfig);
        builder.RegisterComponentInHierarchy<ClimbPointDetector>();
        builder.RegisterComponentInHierarchy<HandMover>();
        builder.RegisterComponentInHierarchy<HandsController>();
        
        builder.Register<DefaultState>(Lifetime.Scoped);
        builder.Register<HookingState>(Lifetime.Scoped);
        builder.Register<CrouchingState>(Lifetime.Scoped);
        builder.Register<PlayerMovementStateMachine>(Lifetime.Scoped);
    }
}
