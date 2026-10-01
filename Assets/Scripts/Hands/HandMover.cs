using System;
using System.Collections.Generic;
using Climbing;
using UnityEngine;
using VContainer;


public class HandMover : MonoBehaviour
{
    [Inject] ClimbConfig climbConfig;
    private Transform lHand, rHand;
    private bool isLHandGrabbing = false, isRHandGrabbing = false;
    private Vector3 lHandStartPos, rHandStartPos;
    private Vector3 lHandTargetPos,  rHandTargetPos;


    public void SetHands(Transform lHand, Transform rHand)
    {
        this.lHand = lHand;
        this.rHand = rHand;
        lHandStartPos = lHand.localPosition;
        rHandStartPos = rHand.localPosition;
    }
    

    public void MoveHandTo(Transform hand, Vector3 pos)
        {
            if (hand == lHand)
            {
                lHandTargetPos = pos;
                isLHandGrabbing = true;
            }
            else if (hand == rHand)
            {
                rHandTargetPos = pos;
                isRHandGrabbing = true;
            }
            hand.SetParent(null, true);
        }

        public void PutHandBack(Transform hand, Transform player)
        {
            if (hand == lHand)
            {
                lHandTargetPos = lHandStartPos;
                isLHandGrabbing = false;
            }
            else if (hand == rHand)
            {
                rHandTargetPos = rHandStartPos;
                isRHandGrabbing = false;
            }
            hand.SetParent(player, true);
        }

        private void Update()
        {
            MoveHand(lHand, lHandTargetPos, isLHandGrabbing);
            MoveHand(rHand, rHandTargetPos, isRHandGrabbing);
        }

        void MoveHand(Transform hand, Vector3 pos, bool isWorldPos)
        {
            if (!isWorldPos)
            {
                hand.localPosition = Vector3.MoveTowards(
                    hand.localPosition,
                    pos,
                    climbConfig.HookSpeed * Time.deltaTime
                );
            }
            else
            {
                hand.position = Vector3.MoveTowards(
                    hand.position,
                    pos,
                    climbConfig.HookSpeed * Time.deltaTime
                );
            }
        }
}

