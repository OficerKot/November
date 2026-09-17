using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Scriptable Objects/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [field:SerializeField]  public float walkSpeed { get; private set; }
    [field: SerializeField] public float runSpeedMultiplier { get; private set; }

    [field: SerializeField] public float crouchSpeedMultiplier { get; private set; }
    [field: SerializeField] public float jumpForce { get; private set; }
}
