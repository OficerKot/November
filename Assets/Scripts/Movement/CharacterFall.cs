using UnityEngine;

public class CharacterFall
{
    float fallStartY;
    public void StartFalling(float posY)
    {
        fallStartY = posY;
    }

    public float GetFallDistance(float posY)
    {
        return fallStartY - posY;
    }
}
