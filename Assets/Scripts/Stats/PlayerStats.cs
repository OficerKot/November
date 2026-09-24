using UnityEngine;

public class PlayerStats
{
    public float health {  get; private set; }
    public float weight { get; private set; }

    public void SetWeight(float weight)
    {
        this.weight = weight;
    }
    public void SetHealth(float health)
    {
        this.health = health;
    }

}
