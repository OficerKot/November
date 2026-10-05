using UnityEngine;

namespace Movement.Player
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Scriptable Objects/Camera Config")]
    public class CameraConfig : ScriptableObject
    {
        [SerializeField] float mouseSensitivity = 0.2f;
        [SerializeField] float maxVerticalRotation = 90f;
        [SerializeField] float minVerticalRotation = -90f;

        public float MouseSensitivity => mouseSensitivity;
        public float MaxVerticalRotation => maxVerticalRotation;
        public float MinVerticalRotation => minVerticalRotation;
    }
}