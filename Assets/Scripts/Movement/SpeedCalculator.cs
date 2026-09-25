using UnityEngine;

public class SpeedCalculator
{
    TemporarySpeedModifier speedModifier;
    PlayerMovementConfig config;
    Vector3 previousTargetDirection;
    float accumulatedAngle;
    struct MovementSpeedParameters
    {
        public float targetSpeed;
        public float acceleration;
        public float brakeAcceleration;
    }
    public SpeedCalculator(TemporarySpeedModifier speedModifier, PlayerMovementConfig config)
    {
        this.speedModifier = speedModifier;
        this.config = config;
    }
    
    public float CalculateSpeed( 
        Vector3 curVelocity,
        Vector3 targetDirection,
        PlayerMovementState state)
    {
        Vector3 horizontalVelocity = curVelocity;
        horizontalVelocity.y = 0f;
        float currentSpeed = horizontalVelocity.magnitude;

        MovementSpeedParameters speedParameters = SetMovementParameters(state);

        speedParameters.targetSpeed *= CalculateAngleMultiplier(targetDirection);
        speedParameters.targetSpeed *= speedModifier.CurMultiplier;

        return CalculateAcceleration(
            currentSpeed,
            speedParameters.targetSpeed,
            speedParameters.acceleration,
            speedParameters.brakeAcceleration);
    }

    MovementSpeedParameters SetMovementParameters(PlayerMovementState state)
    {
        MovementSpeedParameters parameters = new();

        parameters.targetSpeed = config.WalkSpeed;
        parameters.acceleration = config.WalkAcceleration;
        parameters.brakeAcceleration = config.WalkBreakAcceleration;

        if (state.isRunning)
        {
            parameters.targetSpeed *= config.RunSpeedMultiplier;
            parameters.acceleration = config.RunAcceleration;
            parameters.brakeAcceleration *= config.RunBreakAcceleration;
        }
        if (state.isCroucning)
        {
            parameters.targetSpeed *= config.CrouchSpeedMultiplier;
        }

        return parameters;
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
