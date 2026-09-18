using UnityEngine;

public class PlayerEnviropmentDetector : MonoBehaviour
{
    [SerializeField] Transform headDetectorTransform;
    [SerializeField] Transform groundDetectorTransform;
    [SerializeField] LayerMask enviropmentMask;

    [SerializeField] float groundCheckDistance = 0.1f;
    [SerializeField] float headCheckRadius = 0.1f;

    public bool IsGrounded { get; private set; }
    public bool IsHeadBlocked { get; private set; }

    private void Update()
    {
        IsGrounded = CheckGround();
        IsHeadBlocked = CheckHeadBlock();
    }

    private bool CheckGround()
    {
        return Physics.Raycast(
            groundDetectorTransform.position,
            Vector3.down,
            groundCheckDistance,
            enviropmentMask
            );
    }

    private bool CheckHeadBlock()
    {
        return Physics.CheckSphere(
           headDetectorTransform.position,
           headCheckRadius,
           enviropmentMask
           );
    }

    private void OnDrawGizmosSelected()
    {
        if (groundDetectorTransform != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(
                groundDetectorTransform.position,
                groundDetectorTransform.position + Vector3.down * groundCheckDistance
            );
        }

        if (headDetectorTransform != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(
                headDetectorTransform.position,
                headCheckRadius
            );

            Gizmos.DrawLine(
                headDetectorTransform.position,
                headDetectorTransform.position + Vector3.up * headCheckRadius
            );
        }
    }
}
