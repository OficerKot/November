using UnityEngine;

namespace Climbing
{
    [CreateAssetMenu(fileName = "ClimbConfig", menuName = "Scriptable Objects/ClimbConfig")]
    public class ClimbConfig : ScriptableObject
    {
        [SerializeField] private float maxDetectionDistance;
        [SerializeField] private float hookSpeed;
        public float MaxDetectionDistance => maxDetectionDistance;
        public float HookSpeed => hookSpeed;
    }
}