using UnityEngine;

namespace Climbing
{
    [CreateAssetMenu(fileName = "ClimbConfig", menuName = "Scriptable Objects/ClimbConfig")]
    public class ClimbConfig : ScriptableObject
    {
        [Header("Hooking")]
        [SerializeField] private float maxDetectionDistance;
        [SerializeField] private float hookSpeed;
        [Header("Hands")]
        [SerializeField] private float maxHandStamina = 5f;
        [SerializeField] private float staminaRecoverPerSecond = 1f;
        [SerializeField] private float staminaDrainPerSecond = 1f;
        [SerializeField] private float minStaminaToHook = 1f;
        [Header("Movement")]
        [SerializeField] private float jumpForce;

        [SerializeField] private float jumpCooldown;

        public float MaxDetectionDistance => maxDetectionDistance;
        public float HookSpeed => hookSpeed;
        public float MaxHandStamina => maxHandStamina;
        public float StaminaRecoverPerSecond => staminaRecoverPerSecond;
        public float StaminaDrainPerSecond => staminaDrainPerSecond;
        public float MinStaminaToHook => minStaminaToHook;
        public float JumpForce => jumpForce;
        public float JumpCooldown => jumpCooldown;
    }
}