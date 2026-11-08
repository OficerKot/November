using UnityEngine;

public class PlayerMovementState
{
    public bool isRunning = false;
    public bool isJumping = false;
    
    public void Reset()
    {
        isRunning = false;
        isJumping = false;
    }
}
