using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Scriptable Objects/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [Header("Height")]
    [SerializeField] private float defaultCameraHeight;
    [SerializeField] private float defaultColliderHeight;

    [Header("Mouse")]
    [SerializeField] private float mouseSensitivity;

    [Header("Basic movement")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeedMultiplier;
    [SerializeField] private float jumpForce;

    [Header("Crouching")]
    [SerializeField] private float crouchCameraHeight;
    [SerializeField] private float crouchColliderHeight;
    [SerializeField] private float crouchSpeedMultiplier;

    public float DefaultCameraHeight => defaultCameraHeight;
    public float DefaultColliderHeight => defaultColliderHeight;

    public float MouseSensitivity => mouseSensitivity;

    public float WalkSpeed => walkSpeed;
    public float RunSpeedMultiplier => runSpeedMultiplier;
    public float JumpForce => jumpForce;

    public float CrouchCameraHeight => crouchCameraHeight;
    public float CrouchColliderHeight => crouchColliderHeight;
    public float CrouchSpeedMultiplier => crouchSpeedMultiplier;
}