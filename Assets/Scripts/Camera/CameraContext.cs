using Unity.VisualScripting;
using UnityEngine;

public class CameraContext
{
    public Vector2 MouseInput { get; }
    public Transform PlayerTransform { get; }
    public Transform CameraTransform { get; }

    public CameraContext(
        Vector2 mouseInput,
        Transform playerTransform,
        Transform cameraTransform)
    {
        MouseInput = mouseInput;
        PlayerTransform = playerTransform;
        CameraTransform = cameraTransform;
    }
}
