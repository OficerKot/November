using Climbing;
using Hands;
using UnityEngine;
using VContainer;

/// <summary>
/// ѕоиск доступных зацепов перед игроком
/// </summary>
public class ClimbPointDetector : MonoBehaviour
{
    [Inject] private ClimbConfig config;
    
    public IHookable FindClimbPoint()
    {
        Ray ray = Camera.main.ViewportPointToRay(
            new Vector3(0.5f, 0.5f, 0f)
        );
        
        if (Physics.Raycast(ray, out RaycastHit hit, config.MaxDetectionDistance)
            && hit.collider.gameObject.TryGetComponent<IHookable>(out IHookable hookable))
        {
            Debug.Log("Found hookable: " + hit.collider.name);
            return hookable;
        }

        return null;
    }
}
