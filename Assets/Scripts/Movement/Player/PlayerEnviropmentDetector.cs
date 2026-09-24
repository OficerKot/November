using UnityEngine;


public interface IGroundDetector
{
    public bool CheckGround();
    public Vector3 GetGroundNormal();
}

public interface IHeadBlockDetector
{
    public bool CheckHeadBlock();
}

public class PlayerEnviropmentDetector : 
    MonoBehaviour,
    IHeadBlockDetector,
    IGroundDetector
{
    [SerializeField] Transform headDetectorTransform;
    [SerializeField] Transform groundDetectorTransform;
    [SerializeField] LayerMask enviropmentMask;

    [SerializeField] float groundCheckDistance = 0.1f;
    [SerializeField] float headCheckRadius = 0.1f;

    RaycastHit groundHit;

    public bool CheckGround()
    {
        return Physics.Raycast(
            groundDetectorTransform.position,
            Vector3.down,
            out groundHit,
            groundCheckDistance,
            enviropmentMask
            );
    }
    public Vector3 GetGroundNormal()
    {
        return groundHit.normal;
    }

    public bool CheckHeadBlock()
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
