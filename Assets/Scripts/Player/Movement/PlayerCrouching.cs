using UnityEngine;

public class PlayerCrouching : MonoBehaviour
{
    CapsuleCollider playerCollider;


    private void Awake()
    {
        playerCollider = GetComponent<CapsuleCollider>();
    }
    public void Toggle(float colliderHeight, float cameraHeight)
    {
        ChangeCollider(colliderHeight);
    }

    void ChangeCollider(float height)
    {
        playerCollider.height = height;
    }
}
