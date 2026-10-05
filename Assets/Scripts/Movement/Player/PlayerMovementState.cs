using UnityEngine;

public class PlayerMovementState
{
    public bool isRunning = false;
    public bool isJumping = false;
    
    public bool isCroucning = false;
    public bool isMovementBlocked = false;
    
    public void ResetMovementFlags()
    {
        isRunning = false;
        isCroucning = false;
        isJumping = false;
    }
}
