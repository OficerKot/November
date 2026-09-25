using UnityEngine;

public class CharacterFallController : MonoBehaviour
{
    [SerializeField] FallSettings fallSettings;
    
    CharacterFall characterFall;

    IGroundDetector detector;
    IVelocityProvider velocityProvider;
    IDamageable damageable;

    bool isFalling = false;

    private void Awake()
    {
        characterFall = new CharacterFall();

        detector = GetComponent<IGroundDetector>();
        velocityProvider = GetComponent<IVelocityProvider>();
        damageable = GetComponent<IDamageable>();
    }
    private void FixedUpdate()
    {
        bool isGrounded = detector.CheckGround();
        float verticalVelocity = velocityProvider.GetVelocity().y;

        if (!isGrounded && !isFalling && verticalVelocity < fallSettings.MinFallVelocity)
        {
            isFalling = true;
            characterFall.StartFalling(transform.position.y);
        }

        if (isGrounded && isFalling)
        {
            isFalling = false;

            float distance = characterFall.GetFallDistance(transform.position.y);
            CheckFallDamage(distance);
        }
    }

    void CheckFallDamage(float fallDistance)
    {
        if (fallDistance > fallSettings.SafeFallDistance)
        {
            float diff = fallDistance - fallSettings.SafeFallDistance;
            Debug.Log("Distance: " + fallDistance + ", damage: " + diff * fallSettings.DamagePerMeter);
            damageable.TakeDamage(diff * fallSettings.DamagePerMeter);
        }
        else Debug.Log("Fell from a safe height: " + fallDistance );
    }
}
