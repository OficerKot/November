using UnityEngine;

public class PlayerCrouching : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    CapsuleCollider playerCollider;

    Vector3 targetCameraPos;


    private void Awake()
    {
        playerCollider = GetComponent<CapsuleCollider>();
    }
    public void Toggle(float colliderHeight, float cameraHeight)
    {
      //  MoveCamera(cameraHeight);
        ChangeCollider(colliderHeight);
    }

    void MoveCamera(float height)
    {
        cameraTransform.Translate(cameraTransform.position.x, height, cameraTransform.position.z);
    }

    void ChangeCollider(float height)
    {
        playerCollider.height = height;
    }
}
