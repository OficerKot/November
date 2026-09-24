using UnityEngine;

public interface IVelocityProvider
{
    public Vector3 GetVelocity();
}
public class PlayerMovement : MonoBehaviour, IVelocityProvider
{
    private CharacterController controller;
    
    [SerializeField] float gravity = -20f;
    [SerializeField] float groundStick = -2f;
    
    private Vector3 curVelocity;
    float curJumpCooldown = 0;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public void Tick(float deltaTime)
    {
        if (curJumpCooldown > 0f) curJumpCooldown -= deltaTime;

        if (controller.isGrounded && curVelocity.y < 0f)
            curVelocity.y = groundStick;
        else
            curVelocity.y += gravity * deltaTime;

        controller.Move(curVelocity * deltaTime);
    }
    public void Move(Vector3 velocity)
    {
        curVelocity.x = velocity.x;
        curVelocity.z = velocity.z;
    }

    public void Rotate(Vector2 look, float sensitivity)
    {
        transform.Rotate(0f, look.x *sensitivity, 0f);
    }
    public void Jump(float jumpVelocity, float cooldown)
    {
        if (curJumpCooldown > 0f) return;

        Vector3 velocity = curVelocity;
        velocity.y = jumpVelocity;
        curVelocity = velocity;

        curJumpCooldown = cooldown;
    }

    public Vector3 GetVelocity() => curVelocity;
    public bool IsGrounded => controller.isGrounded;
}
