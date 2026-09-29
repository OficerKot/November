using System;
using System.Collections.Generic;
using Climbing;
using UnityEngine;
using VContainer;


public class HandMover : MonoBehaviour
{
    [Inject] ClimbConfig climbConfig;
    private Transform lHand, rHand;
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
            }
            else if (hand == rHand)
            {
                rHandTargetPos = pos;
            }
        }

        public void PutHandBack(Transform hand)
        {
            if (hand == lHand)
            {
                lHandTargetPos = lHandStartPos;
            }
            else if (hand == rHand)
            {
                rHandTargetPos = rHandStartPos;
            }
        }

        private void Update()
        {
            MoveHand(lHand, lHandTargetPos);
            MoveHand(rHand, rHandTargetPos);
        }

        void MoveHand(Transform hand, Vector3 pos)
        {
            hand.localPosition = Vector3.MoveTowards(
                hand.localPosition,
                pos,
                climbConfig.HookSpeed * Time.deltaTime
            );
        }
}

