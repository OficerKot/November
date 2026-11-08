using UnityEngine;
using VContainer;

public class CharacterFallController : MonoBehaviour
{
    [SerializeField] FallSettings fallSettings;
    [Inject] private IStateSwitcher stateSwitcher;
    FallVelocityTracker fallVelocityTracker;
    
    IGroundDetector detector;
    IVelocityProvider velocityProvider;
    IDamageable damageable;

    bool isFalling = false;

    private void Awake()
    {
        fallVelocityTracker = new FallVelocityTracker();
        detector = GetComponent<IGroundDetector>();
        velocityProvider = GetComponent<IVelocityProvider>();
        damageable = GetComponent<IDamageable>();
    }
    private void FixedUpdate()
    {
        bool isGrounded = detector.CheckGround();
        Vector3 verticalVelocity = velocityProvider.GetVelocity();

        if (isFalling)
        {
            if (verticalVelocity.y < 0f)
            {
                fallVelocityTracker.UpdateVelocity(verticalVelocity);
            }
        }
        if (!isGrounded && !isFalling && verticalVelocity.y < fallSettings.MinFallVelocity)
        {
            isFalling = true;
        }
        if (isGrounded && isFalling && verticalVelocity.y >= 0f)
        {
            StopFall();
        }
    }
    

    private void StopFall()
    {
        isFalling = false;
        CheckFallDamage(fallVelocityTracker.FallingVelocity);
    }
    private void CheckFallDamage(Vector3 fallVelocity)
    {
        float fallSpeed = Mathf.Max(0f, -fallVelocity.y);
        if (fallSpeed > fallSettings.SafeFallSpeed)
        {
            float diff = fallSpeed - fallSettings.SafeFallSpeed;
            damageable.TakeDamage(diff * fallSettings.DamageMultipler);
            Debug.Log("Speed: " + fallSpeed + ", damage: " + diff * fallSettings.DamageMultipler);
        }
        else Debug.Log("Fell with a safe speed: " + fallSpeed );
    }
    
}
