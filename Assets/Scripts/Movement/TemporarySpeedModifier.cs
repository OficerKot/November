using UnityEngine;

/// <summary>
/// Смена скорости при временных событиях
/// </summary>
public class TemporarySpeedModifier
{
    float timer = 0f;
    float curMultipler;

    public float CurMultiplier => curMultipler;
    public void ChangeSpeed(float multipler, float time)
    {
        timer = time;
        curMultipler = multipler;
    }

    public void Tick(float deltaTime)
    {
        timer -= deltaTime;

        if (timer <= 0f)
        {
            curMultipler = 1f;
            timer = 0f;
        }
    }


}
