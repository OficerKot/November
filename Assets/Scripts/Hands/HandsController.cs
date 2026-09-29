using System;
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
        if (hand.IsHooking)
        {
            eventBus.Publish(new OnClimbEvent(false)); 
        }
        handMover.PutHandBack(hand.transform);
        hand.Release();
    }

    void TryPerformHook(Hand hand)
    {
        IHookable hookable = climbDetector.FindClimbPoint();
        if(hookable == null) return;
        
        Vector3 targetLocalPosition = transform.InverseTransformPoint(hookable.GetGrabPosition());
        handMover.MoveHandTo(hand.transform, targetLocalPosition);
        hand.Hook(hookable);
        eventBus.Publish(new OnClimbEvent(true));
    }


}
