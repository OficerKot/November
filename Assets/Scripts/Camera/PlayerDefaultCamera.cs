using Movement.Player;
using UnityEngine;
using VContainer;

public class PlayerDefaultCamera : IPlayerCamera
{
    private CameraConfig config;
    private float verticalRotation = 0f;

    public PlayerDefaultCamera(CameraConfig config)
    {
        this.config = config;
    }
    public void LookAround(CameraContext context)
    {
        Vector2 mouseInput = context.MouseInput;
        Transform playerTransform = context.PlayerTransform;
        Transform cameraTransform = context.CameraTransform;

        float mouseX = mouseInput.x * config.MouseSensitivity;
        float mouseY = mouseInput.y * config.MouseSensitivity;

        playerTransform.Rotate(Vector3.up * mouseX);

        verticalRotation -= mouseY;

        verticalRotation = Mathf.Clamp(
            verticalRotation,
            config.MinVerticalRotation,
            config.MaxVerticalRotation);

        context.CameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }
}

