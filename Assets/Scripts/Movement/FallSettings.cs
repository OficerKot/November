using UnityEngine;

[CreateAssetMenu(fileName = "FallSettings", menuName = "Scriptable Objects/FallSettings")]
public class FallSettings : ScriptableObject
{
    [SerializeField] float damageMultipler;
    [SerializeField] float safeFallSpeed;
    [SerializeField] float minFallVelocity = -0.5f;

    public float DamageMultipler => damageMultipler;
    public float SafeFallSpeed => safeFallSpeed;
    public float MinFallVelocity => minFallVelocity;
}
