
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float animationDampTime = 0.1f;

    private PlayerMovement movement;

    private static readonly int SpeedHash =
        Animator.StringToHash("Speed");

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (movement == null || animator == null)
            return;

        Vector3 velocity = movement.GetVelocity();

        // Убираем вертикальную скорость (гравитацию и прыжки)
        float horizontalSpeed = new Vector3(
            velocity.x,
            0f,
            velocity.z
        ).magnitude;

        animator.SetFloat(
            SpeedHash,
            horizontalSpeed,
            animationDampTime,
            Time.deltaTime
        );
    }
}
