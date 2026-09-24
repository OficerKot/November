using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatsConfig", menuName = "Scriptable Objects/PlayerStatsConfig")]
public class PlayerStatsConfig : ScriptableObject
{
    [SerializeField] float maxHealth;
    [SerializeField] float minHealth;

    [SerializeField] float maxWeight;

    public float MaxHealth => maxHealth;
    public float MinHealth => minHealth;
    public float MaxWeight => maxWeight;
}
