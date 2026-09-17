using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Move(Vector2 value, float speed)
    {
        Vector3 dir = transform.right * value.x + transform.forward * value.y;

        dir.Normalize();

        Vector3 movement = dir * speed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + movement);
    }

    public void Rotate(Vector2 look, float sensitivity)
    {
        float rotation = look.x * sensitivity;

        Quaternion deltaRotation =
            Quaternion.Euler(0f, rotation, 0f);

        rb.MoveRotation(rb.rotation * deltaRotation);
    }
    public void Jump(float force)
    {
        rb.AddForce(Vector3.up * force, ForceMode.Impulse);
    }
}
