using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    [Header("Roots")]
    [SerializeField] private bool useRandomSeed = true;
    [SerializeField] private int seed = 12345;

    [SerializeField] private Transform slotsRoot;       
    [SerializeField] private Transform generatedRoot;   

    [Header("Debug")]
    [SerializeField] private Transform[] originalRoots;

    [ContextMenu("Generate")]
    public void Generate()
    {
        if (slotsRoot == null)
        {
            Debug.LogError("slotsRoot isn't assigned.");
            return;
        }
        if (generatedRoot == null)
        {
            Debug.LogError("generatedRoot isn't assigned.");
            return;
        }

        if (useRandomSeed)
        {
            seed = Random.Range(0, 100000);
        }
        
        Random.InitState(seed);
        
        Debug.Log($"Seed generation: {seed}");

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

        Debug.Log($"Walls generated: {spawned}");
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

        [ContextMenu("Hide Originals")]
    public void HideOriginals()
    {
        SetOriginalsActive(false);
    }

    [ContextMenu("Show Originals")]
    public void ShowOriginals()
    {
        SetOriginalsActive(true);
    }

    private void SetOriginalsActive(bool isActive)
    {
        if (originalRoots == null)
            return;

        foreach (var root in originalRoots)
        {
            if (root != null)
                root.gameObject.SetActive(isActive);
        }
    }
}