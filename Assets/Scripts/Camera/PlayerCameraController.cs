using UnityEngine;
using UnityEngine.Windows;
using VContainer;

/// <summary>
/// Контроллер камеры. Позволяет менять стратегию управления камеры и двигать камеру.
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
        strategy.SyncRotationAngles(transform);
    }

    public void SetHangingStrategy()
    {
        strategy = hangingCamera;
        strategy.SyncRotationAngles(transform);
    }

    /// <summary>
    /// Поворот камеры с учётом текущей стратегии управления
    /// </summary>
    /// <param name="input">Ввод с мыши</param>
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