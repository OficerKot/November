using System;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    private IPlayerCamera camera;
    private PlayerInput input;
    private Transform playerTransform;
    private Transform cameraTransform;

    private void Awake()
    {
        camera = GetComponentInChildren<IPlayerCamera>();
        input = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        CameraContext context = new CameraContext(
            input.lookAxis,
            playerTransform,
            cameraTransform
        );

        camera.LookAround(context);
    }
}
