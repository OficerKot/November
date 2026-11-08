using UnityEngine;

public class FallVelocityTracker
{
    private Vector3 recentFallingVelocity;
    public Vector3 FallingVelocity => recentFallingVelocity;

    public void UpdateVelocity(Vector3 velocity)
    {
        if (velocity.y < 0f)
        {
            recentFallingVelocity =  velocity;
        }
    }

}
