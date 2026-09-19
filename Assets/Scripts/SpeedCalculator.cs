using UnityEngine;

public static class SpeedCalculator
{
    public static float CalculateSpeed(float curSpeed, float targetSpeed, float acceleration)
    {
        if (acceleration <= 0f)
        {
            return targetSpeed;
        }
        else
        {
            return Mathf.MoveTowards(
                curSpeed,
                targetSpeed,
                acceleration * Time.fixedDeltaTime
            );
        }
    }
}
