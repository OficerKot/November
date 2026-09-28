using Hands;
using UnityEngine;

public class SimpleHandle : MonoBehaviour, IHookable
{
    public Vector3 GetGrabPosition()
    {
        return transform.position;
    }
}
