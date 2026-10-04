using Climbing;
using Events;
using Hands;
using UnityEngine;
using VContainer;

public class HandsController : MonoBehaviour
{
    private PlayerInput input;
    private ClimbPointDetector climbDetector;
    private HandMover handMover;
    [SerializeField] Hand leftHand, rightHand;
    [Inject] private IEventBus eventBus;
    [Inject] private ClimbConfig config;
    

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        climbDetector = GetComponent<ClimbPointDetector>();
        handMover = GetComponent<HandMover>();
        
        handMover.SetHands(leftHand.transform, rightHand.transform);
        leftHand.Init(config.MaxHandStamina, config.MinStaminaToHook);
        rightHand.Init(config.MaxHandStamina, config.MinStaminaToHook);
    }

    private void FixedUpdate()
    {
        UpdateHandsState();
        
        if(rightHand.IsActive && rightHand.CanHook) TryPerformHook(rightHand);
        if(leftHand.IsActive && leftHand.CanHook) TryPerformHook(leftHand);
        
    }

    void UpdateHandsState()
    {
        ToggleHand(rightHand, input.IsRightHandActive);
        ToggleHand(leftHand, input.IsLeftHandActive);

        UpdateHandStamina(rightHand);
        UpdateHandStamina(leftHand);
        
    }
    
    void UpdateHandStamina(Hand hand)
    {
        if (hand.IsHooking)
        {
            hand.DrainStamina(config.StaminaDrainPerSecond, Time.fixedDeltaTime);
            if (hand.IsFalling) ReleaseHand(hand);
        }
        else
        {
            hand.RecoverStamina(config.StaminaRecoverPerSecond, Time.fixedDeltaTime);
        }
        
    }
    void ToggleHand(Hand hand, bool isActive)
    {
        hand.Toggle(isActive);
        if(!isActive)
        {
            ReleaseHand(hand);
        }
    }
    

    void ReleaseHand(Hand hand)
    {
        handMover.PutHandBack(hand.transform, transform);
        hand.Release();
        
        if (!leftHand.IsHooking && !rightHand.IsHooking)
        {
            eventBus.Publish(new OnClimbEvent(false)); 
        }
    }

    void TryPerformHook(Hand hand)
    {
        if (hand.IsHooking) return;
        
        IHookable hookable = climbDetector.FindClimbPoint();
        if(hookable == null) return;
        
        handMover.MoveHandTo(hand.transform, hookable.GetGrabPosition());
        hand.Hook(hookable);
        eventBus.Publish(new OnClimbEvent(true));
    }
}
