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
    [Inject] private IStateSwitcher stateSwitcher;
    [Inject] private ClimbConfig config;
    

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        climbDetector = GetComponent<ClimbPointDetector>();
        handMover = GetComponent<HandMover>();
        
        handMover.SetHands(leftHand.transform, rightHand.transform);
        leftHand.Init(config.MaxHandStamina, config.MinStaminaToHook);
        rightHand.Init(config.MaxHandStamina, config.MinStaminaToHook);
        
        handMover.PutHandBack(leftHand.transform, transform);
        handMover.PutHandBack(rightHand.transform, transform);
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
            if (hand.IsFalling) DeactivateHand(hand);
        }
        else
        {
            hand.RecoverStamina(config.StaminaRecoverPerSecond, Time.fixedDeltaTime);
        }
        
    }
    void ToggleHand(Hand hand, bool isActive)
    {
        if(!isActive && hand.IsActive)
        {
            DeactivateHand(hand);
        }
        hand.Toggle(isActive);
    }

    private void DeactivateHand(Hand hand)
    {
        bool wasHooking = hand.IsHooking;
        
        handMover.PutHandBack(hand.transform, transform);
        hand.Release();
        
        if (wasHooking && !leftHand.IsHooking && !rightHand.IsHooking)
        { 
            stateSwitcher.SwitchState(typeof(DefaultState));
        }
    }
   

    void TryPerformHook(Hand hand)
    {
        if (hand.IsHooking) return;
        
        IHookable hookable = climbDetector.FindClimbPoint();
        if(hookable == null) return;
        
        handMover.MoveHandTo(hand.transform, hookable.GetGrabPosition());
        hand.Hook(hookable);
        stateSwitcher.SwitchState(typeof(HangingState));
    }
}
