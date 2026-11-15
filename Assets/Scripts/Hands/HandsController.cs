using Climbing;
using Events;
using Hands;
using UnityEngine;
using UnityEngine.XR;
using VContainer;

public class HandsController : MonoBehaviour
{
    private PlayerInput input;
    private ClimbPointDetector climbDetector;
    private HandMover handMover;
 
    private IStateSwitcher stateSwitcher;
    private ClimbConfig config;
    private JointCreator jointCreator;

    [SerializeField] Hand leftHand, rightHand;

    [Inject]
    public void Init(
        IStateSwitcher stateSwitcher,
        ClimbConfig config,
        JointCreator jointCreator)
    {
        this.stateSwitcher = stateSwitcher;
        this.config = config;
        this.jointCreator = jointCreator;
    }
    

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
            jointCreator.Detach();
            stateSwitcher.SwitchState(typeof(DefaultState));
        }
    }
   

    void TryPerformHook(Hand hand)
    {
        if (hand.IsHooking) return;
        
        IHookable hookable = climbDetector.FindClimbPoint();
        if(hookable == null) return;

        PerformHook(hand, hookable);
    }

    void PerformHook(Hand hand, IHookable hookable)
    {
        handMover.MoveHandTo(hand.transform, hookable.GetGrabPosition());
        hand.Hook(hookable);

        jointCreator.Detach();
        jointCreator.Attach(hookable.GetGrabPosition());

        if (!(leftHand.IsHooking && rightHand.IsHooking)) // ≈сли уже держались одной рукой - состо€ние то же
        {
            stateSwitcher.SwitchState(typeof(HangingState));
        }
    }
}
