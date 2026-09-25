using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Scriptable Objects/PlayerMovementConfig")]
public class PlayerMovementConfig : ScriptableObject
{
    [FormerlySerializedAs("defaultColliderHeight")]
    [Header("Height")]
    [SerializeField] private float defaultPlayerHeight;

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

    [FormerlySerializedAs("crouchColliderHeight")]
    [Header("Crouching")]
    [SerializeField] private float crouchPlayerHeight;
    [SerializeField] private float crouchSpeedMultiplier;

    public float DefaultPlayerHeight => defaultPlayerHeight;

    public float MouseSensitivity => mouseSensitivity;

    public float WalkSpeed => walkSpeed;
    public float WalkAcceleration => walkAcceleration;
    public float WalkBreakAcceleration => walkBrakeAcceleration;
    public float RunSpeedMultiplier => runSpeedMultiplier;
    public float RunAcceleration => runAcceleration;
    public float RunBreakAcceleration => runBrakeAcceleration;
    public float JumpVelocity => jumpVelocity;

    public float CrouchPlayerHeight => crouchPlayerHeight;
    public float CrouchSpeedMultiplier => crouchSpeedMultiplier;

    public float JumpCooldown => jumpCooldown;
}