using UnityEngine;
using VContainer;

/// <summary>
///  онтроллер камеры. ѕозвол€ет мен€ть стратегию управлени€ камеры и двигать камеру.
/// </summary>
public class PlayerCameraController : MonoBehaviour
{
    private PlayerDefaultCamera defaultCamera;
    private PlayerHangingCamera hangingCamera;
    private IPlayerCamera strategy;

    [Inject]
    public void Construct(
        PlayerDefaultCamera defaultCamera,
        PlayerHangingCamera hangingCamera)
    {
        this.defaultCamera = defaultCamera;
        this.hangingCamera = hangingCamera;

        strategy = defaultCamera;
    }
    public void SetDefaultStrategy()
    {
        strategy = defaultCamera;
    }

    public void SetHangingStrategy()
    {
        strategy = hangingCamera;
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