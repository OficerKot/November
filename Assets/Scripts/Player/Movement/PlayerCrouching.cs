using UnityEngine;

public class PlayerCrouching : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    Collider playerCollider;

    //вынести
    float crouchCameraHeight = 0.3f;
    float crouchColliderHeight = 0.3f;

    void Crouch()
    {
      
    }

    void Stand()
    {

    }
}
