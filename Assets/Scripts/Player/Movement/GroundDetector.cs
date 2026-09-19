using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    private BoxCollider groundCollider;
    public bool IsGrounded { get; private set; }

    private void Awake()
    {
        groundCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            return;

        IsGrounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            return;

        IsGrounded = false;
    }
}
