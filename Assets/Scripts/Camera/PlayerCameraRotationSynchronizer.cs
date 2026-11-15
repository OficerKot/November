using UnityEngine;

public static class PlayerCameraRotationSynchronizer
{
    /// <summary>
    /// Поворачивает тело туда же, куда смотрит камера. Игнорирует наклон вверх/вниз
    /// </summary>
    public static void RotateBodyToCamera(Transform body, Transform camera)
    {
        Quaternion targetWorldRotation = camera.rotation;

        // Убираем вертикальную составляющую направления
        Vector3 direction = Vector3.ProjectOnPlane(
            camera.forward,
            Vector3.up
        );

        if (direction.sqrMagnitude < 0.0001f)
            return;

        body.rotation = Quaternion.LookRotation(
            direction,
            Vector3.up
        );

        if (camera.IsChildOf(body))
        {
            camera.rotation = targetWorldRotation;
        }
    }
}
