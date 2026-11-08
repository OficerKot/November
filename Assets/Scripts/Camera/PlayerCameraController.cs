using System;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    private PlayerCamera camera;
    private PlayerInput input;

    private void Awake()
    {
        camera = GetComponentInChildren<PlayerCamera>();
        input = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        camera.LookAround(input.lookAxis, transform);
    }
}
