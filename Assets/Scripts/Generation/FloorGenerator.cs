using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    [Header("Корни сцены")]
    [SerializeField] private Transform slotsRoot;       // Slots/
    [SerializeField] private Transform generatedRoot;   // Slots/GeneratedWalls

    [ContextMenu("Generate")]
    public void Generate()
    {
        if (slotsRoot == null)
        {
            Debug.LogError("slotsRoot не назначен.");
            return;
        }
        if (generatedRoot == null)
        {
            Debug.LogError("generatedRoot не назначен.");
            return;
        }

        ClearGenerated();

        var slots = slotsRoot.GetComponentsInChildren<SlotScript>(includeInactive: false);
        Debug.Log($"Найдено WallSlot: {slots.Length}");

        int spawned = 0;
        foreach (var slot in slots)
        {
            var variants = slot.Variants;
            if (variants == null || variants.Length == 0)
                continue;

            int index = Random.Range(0, variants.Length);
            var prefab = variants[index];
            if (prefab == null)
                continue;

            var instance = Instantiate(prefab);
            instance.name = prefab.name;
            instance.transform.SetParent(generatedRoot, true);
            instance.transform.position = slot.transform.position;
            instance.transform.rotation = slot.transform.rotation;

            spawned++;
        }

        Debug.Log($"Сгенерировано стен: {spawned}");
    }

    [ContextMenu("Clear Generated")]
    public void ClearGenerated()
    {
        if (generatedRoot == null)
            return;

        for (int i = generatedRoot.childCount - 1; i >= 0; i--)
        {
            var child = generatedRoot.GetChild(i).gameObject;
#if UNITY_EDITOR
            if (!Application.isPlaying)
                DestroyImmediate(child);
            else
                Destroy(child);
#else
            Destroy(child);
#endif
        }
    }
}