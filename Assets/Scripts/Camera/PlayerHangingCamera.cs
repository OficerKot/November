using Movement.Player;
using UnityEngine;
using VContainer;

public class PlayerHangingCamera : IPlayerCamera
{
    private CameraConfig config;
    private float verticalRotation = 0f;
    private float horizontalRotation = 0f;

    public PlayerHangingCamera(CameraConfig config)
    {
        this.config = config;
    }
    public void LookAround(CameraContext context)
    {
        Vector2 mouseInput = context.MouseInput;

        float mouseX = mouseInput.x * config.MouseSensitivity;
        float mouseY = mouseInput.y * config.MouseSensitivity;

        horizontalRotation += mouseX;
        verticalRotation -= mouseY;

        verticalRotation = Mathf.Clamp(
            verticalRotation,
            config.MinVerticalRotation,
            config.MaxVerticalRotation);

        context.CameraTransform.localRotation = Quaternion.Euler(
            verticalRotation,
            horizontalRotation,
            0f);
    }
}
