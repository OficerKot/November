using UnityEngine;

/// <summary>
/// Стратегия поведения камеры игрока
/// </summary>
public interface IPlayerCamera
{
    public void LookAround(CameraContext context);
    public void SyncRotationAngles(Transform cameraTransform);
}
