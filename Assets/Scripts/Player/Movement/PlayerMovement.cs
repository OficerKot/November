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
        Vector3 dir = new Vector3(value.x, 0f, value.y).normalized;
        dir = speed * dir * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + dir);
    }

    public void Jump(float force)
    {
        rb.AddForce(Vector3.up * force, ForceMode.Impulse);
    }
}
