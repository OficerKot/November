using UnityEngine;

[CreateAssetMenu(fileName = "FallSettings", menuName = "Scriptable Objects/FallSettings")]
public class FallSettings : ScriptableObject
{
    [SerializeField] float damagePerMeter;
    [SerializeField] float safeFallDistance;
    [SerializeField] float minFallVelocity = -0.5f;

    public float DamagePerMeter => damagePerMeter;
    public float SafeFallDistance => safeFallDistance;
    public float MinFallVelocity => minFallVelocity;
}
