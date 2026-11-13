using UnityEngine;

/// <summary>
///  онтроллер камеры. ѕозвол€ет мен€ть стратегию управлени€ камеры и двигать камеру.
/// </summary>
public class PlayerCameraController : MonoBehaviour
{
    private IPlayerCamera strategy;

    public void SetStrategy(IPlayerCamera strategy)
    {
        this.strategy = strategy;
    }

    public void LookAround(Vector2 input, Transform playerTransform)
    {
        CameraContext context = new CameraContext(
            input,
            playerTransform,
            transform
        );
   
        strategy.LookAround(context);
    }
}