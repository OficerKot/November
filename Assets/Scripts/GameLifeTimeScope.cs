using Climbing;
using Events;
using Movement;
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

        RegisterStats(builder);
        RegisterCamera(builder);
        RegisterHands(builder);
        RegisterStates(builder);
        RegisterMovement(builder);
    }

    void RegisterStats(IContainerBuilder builder)
    {
        builder.RegisterInstance(playerStatsConfig);
        builder.RegisterComponentInHierarchy<PlayerStatsController>();
        builder.RegisterComponentInHierarchy<HealthView>();
    }
    void RegisterMovement(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<PlayerInput>();
        builder.RegisterComponentInHierarchy<PlayerMovement>();

        builder.RegisterComponentInHierarchy<PlayerMovementController>();
        builder.RegisterInstance(playerMovementConfig);

        builder.RegisterComponentInHierarchy<PlayerCrouching>();
        builder.RegisterComponentInHierarchy<CharacterFallController>();
        builder.RegisterComponentInHierarchy<PlayerEnviropmentDetector>()
            .As<IHeadBlockDetector>()
            .As<IGroundDetector>();
        builder.RegisterComponentInHierarchy<HangingMovement>();

        builder.Register<TemporarySpeedModifier>(Lifetime.Scoped);
        builder.Register<SpeedCalculator>(Lifetime.Scoped);
    }
    void RegisterHands(IContainerBuilder builder)
    {
        builder.RegisterInstance(climbConfig);
        builder.RegisterComponentInHierarchy<ClimbPointDetector>();
        builder.RegisterComponentInHierarchy<HandMover>();
        builder.RegisterComponentInHierarchy<HandsController>();
    }

    void RegisterCamera(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<PlayerDefaultCamera>(); //!!!
        builder.RegisterInstance(cameraConfig);
    }
    
    void RegisterStates(IContainerBuilder builder)
    {
        builder.Register<DefaultState>(Lifetime.Scoped);
        builder.Register<HangingState>(Lifetime.Scoped);
        builder.Register<CrouchingState>(Lifetime.Scoped);

        builder.Register<PlayerMovementStateMachine>(Lifetime.Scoped)
         .AsSelf()
         .As<IStateSwitcher>();
    }
}
