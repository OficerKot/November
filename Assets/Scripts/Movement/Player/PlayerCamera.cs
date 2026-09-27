using Movement.Player;
using UnityEngine;
using VContainer;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform playerBody;
    [Inject] private CameraConfig config;
    private float verticalRotation = 0f;
    
    public void LookAround(Vector2 mouseInput)
    {
        float mouseX = mouseInput.x * config.MouseSensitivity;
        float mouseY = mouseInput.y * config.MouseSensitivity;

        playerBody.Rotate(Vector3.up * mouseX);

        verticalRotation -= mouseY;
        
        verticalRotation = Mathf.Clamp(
            verticalRotation,
            config.MinVerticalRotation,
            config.MaxVerticalRotation);
        
        transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }
}

