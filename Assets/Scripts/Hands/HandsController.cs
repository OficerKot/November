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
    

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        climbDetector = GetComponent<ClimbPointDetector>();
        handMover = GetComponent<HandMover>();
        handMover.SetHands(leftHand.transform, rightHand.transform);
    }

    private void FixedUpdate()
    {
        UpdateHand(rightHand, input.IsRightHandActive);
        UpdateHand(leftHand, input.IsLeftHandActive);
        
        if(rightHand.IsActive) TryPerformHook(rightHand);
        if(leftHand.IsActive) TryPerformHook(leftHand);
        
    }

    void UpdateHand(Hand hand, bool isActive)
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
