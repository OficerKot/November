using Movement.Player;
using UnityEngine;
using VContainer;

public class PlayerHangingCamera : IPlayerCamera
{
    [Inject] private CameraConfig config;

    private float verticalRotation = 0f;
    private float horizontalRotation = 0f;
    public void LookAround(CameraContext context)
    {
        Vector2 mouseInput = context.MouseInput;
        Transform cameraTransform = context.CameraTransform;

        float mouseX = mouseInput.x * config.MouseSensitivity;
        float mouseY = mouseInput.y * config.MouseSensitivity;

        horizontalRotation += mouseX;
        verticalRotation -= mouseY;

        verticalRotation = Mathf.Clamp(
            verticalRotation,
            config.MinVerticalRotation,
            config.MaxVerticalRotation);

        cameraTransform.localRotation = Quaternion.Euler(
            verticalRotation,
            horizontalRotation,
            0f);
    }
}
