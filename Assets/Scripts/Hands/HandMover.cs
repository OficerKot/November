using Climbing;
using UnityEngine;
using VContainer;


public class HandMover : MonoBehaviour
{
    [Inject] ClimbConfig climbConfig;
    
    public void MoveHandTo(Transform hand, Vector3 pos)
    {
        hand.localPosition = Vector3.MoveTowards(
            hand.localPosition,
            pos,
            climbConfig.HookSpeed * Time.deltaTime
        );
    }
}
