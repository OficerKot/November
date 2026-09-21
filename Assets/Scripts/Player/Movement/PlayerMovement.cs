using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    float jumpCooldown = 0;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (jumpCooldown > 0f)
        {
            jumpCooldown -= Time.fixedDeltaTime;
        }
    }
    public void Move(Vector2 input, float speed)
    {
        Vector3 dir =
        transform.right * input.x +
        transform.forward * input.y;

        dir.Normalize();

        Vector3 velocity = rb.linearVelocity;
        Vector3 horizontalVelocity = dir * speed;

        velocity.x = horizontalVelocity.x;
        velocity.z = horizontalVelocity.z;

        Debug.Log("Speed:" + speed);
        rb.linearVelocity = velocity;
    }

    public void Rotate(Vector2 look, float sensitivity)
    {
        float rotation = look.x * sensitivity;

        Quaternion deltaRotation =
            Quaternion.Euler(0f, rotation, 0f);

        rb.MoveRotation(rb.rotation * deltaRotation);
    }
    public void Jump(float jumpVelocity, float cooldown)
    {
        if (jumpCooldown > 0f) return;

        Vector3 velocity = rb.linearVelocity;
        velocity.y = jumpVelocity;
        rb.linearVelocity = velocity;

        jumpCooldown = cooldown;
    }

    public Vector3 GetVelocity()
    {
        return rb.linearVelocity;
    }
}
