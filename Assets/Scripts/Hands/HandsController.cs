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
    }

    private void FixedUpdate()
    {
        UpdateHand(rightHand, input.IsRightHandActive);
        UpdateHand(leftHand, input.IsLeftHandActive);
        
        if(rightHand.IsActive) FindHookable(rightHand);
        if(leftHand.IsActive) FindHookable(leftHand);
    }

    void UpdateHand(Hand hand, bool isActive)
    {
        hand.Toggle(isActive);
        if(!isActive)
        {
            ReleaseHand(hand);
            handMover.MoveHandTo(hand.transform, hand.StartPost);
        }
    }

    void ReleaseHand(Hand hand)
    {
        if (hand.IsHooking)
        {
            eventBus.Publish(new OnClimbEvent(false)); 
        }
        hand.Release();
    }
    void FindHookable(Hand hand)
    {
        IHookable climbable = climbDetector.FindClimbPoint();
        if (climbable != null)
        {
            Vector3 targetLocalPosition = transform.InverseTransformPoint(climbable.GetGrabPosition());
            handMover.MoveHandTo(hand.transform, targetLocalPosition);
            hand.Hook(climbable);
            eventBus.Publish(new OnClimbEvent(true));
        }
    }


}
