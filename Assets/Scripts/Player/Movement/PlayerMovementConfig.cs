using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Scriptable Objects/PlayerMovementConfig")]
public class PlayerMovementConfig : ScriptableObject
{
    [Header("Height")]
    [SerializeField] private float defaultCameraHeight;
    [SerializeField] private float defaultColliderHeight;

    [Header("Mouse")]
    [SerializeField] private float mouseSensitivity;

    [Header("Walking")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float walkAcceleration;
    [SerializeField] private float walkBrakeAcceleration;

    [Header("Running")]
    [SerializeField] private float runSpeedMultiplier;
    [SerializeField] private float runAcceleration;
    [SerializeField] private float runBrakeAcceleration;

    [Header("Jumping")]
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float jumpCooldown;

    [Header("Crouching")]
    [SerializeField] private float crouchCameraHeight;
    [SerializeField] private float crouchColliderHeight;
    [SerializeField] private float crouchSpeedMultiplier;

    public float DefaultCameraHeight => defaultCameraHeight;
    public float DefaultColliderHeight => defaultColliderHeight;

    public float MouseSensitivity => mouseSensitivity;

    public float WalkSpeed => walkSpeed;
    public float WalkAcceleration => walkAcceleration;
    public float WalkBreakAcceleration => walkBrakeAcceleration;
    public float RunSpeedMultiplier => runSpeedMultiplier;
    public float RunAcceleration => runAcceleration;
    public float RunBreakAcceleration => runBrakeAcceleration;
    public float JumpVelocity => jumpVelocity;

    public float CrouchCameraHeight => crouchCameraHeight;
    public float CrouchColliderHeight => crouchColliderHeight;
    public float CrouchSpeedMultiplier => crouchSpeedMultiplier;

    public float JumpCooldown => jumpCooldown;
}