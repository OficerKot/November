using UnityEngine;

public class SpeedCalculator
{
    private readonly TemporarySpeedModifier speedModifier = new TemporarySpeedModifier();
    private Vector3 previousTargetDirection;
    private float accumulatedAngle;
    public class MovementSpeedParameters
    {
        public float targetSpeed;
        public float acceleration;
        public float brakeAcceleration;
        public float minSpeed;

        public MovementSpeedParameters(
            float targetSpeed, 
            float acceleration,
            float brakeAcceleration,
            float minSpeed)
        {
            this.targetSpeed = targetSpeed;
            this.acceleration = acceleration;
            this.brakeAcceleration = brakeAcceleration;
            this.minSpeed = minSpeed;
        }
    }
    public void Tick(float deltaTime)
    {
        speedModifier.Tick(deltaTime);
    }
    
    public float CalculateSpeed( 
        Vector3 curVelocity,
        Vector3 targetDirection,
        MovementSpeedParameters speedParameters)
    {
        Vector3 horizontalVelocity = curVelocity;
        horizontalVelocity.y = 0f;
        float currentSpeed = horizontalVelocity.magnitude;

        speedParameters.targetSpeed *= CalculateAngleMultiplier(targetDirection);
        speedParameters.targetSpeed *= speedModifier.CurMultiplier;

        return Mathf.Max(
            CalculateAcceleration(
                currentSpeed,
                speedParameters.targetSpeed,
                speedParameters.acceleration,
                speedParameters.brakeAcceleration),
            speedParameters.minSpeed);
    }
    
    float CalculateAngleMultiplier(Vector3 targetDirection)
    {
        if (targetDirection.sqrMagnitude <= 0.001f)
        {
            accumulatedAngle = 0f;
            return 1f;
        }

        targetDirection.Normalize();

        if (previousTargetDirection.sqrMagnitude <= 0.001f)
        {
            previousTargetDirection = targetDirection;
            return 1f;
        }

        float angle = Vector3.Angle(
            previousTargetDirection,
            targetDirection
        );
        accumulatedAngle = angle > 0.001f? accumulatedAngle + angle : 0f;

        previousTargetDirection = targetDirection;

        return Mathf.InverseLerp(180f, 0f, accumulatedAngle);
    }
    public float CalculateAcceleration(
        float curSpeed,
        float targetSpeed,
        float acceleration,
        float brakeAcceleration)
    {
        if (acceleration <= 0f)
        {
            return targetSpeed;
        }
        else
        {
            float usedAcceleration = targetSpeed < curSpeed
            ? brakeAcceleration
            : acceleration;

            return Mathf.MoveTowards(
                curSpeed,
                targetSpeed,
                usedAcceleration * Time.fixedDeltaTime // потом лучше вынести в параметр 
            );
        }
    }
}
