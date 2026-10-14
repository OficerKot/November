using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class SlotMapping
{
    [Tooltip("Контейнер с оригинальными стенами. Например: OuterWalls 2x1")]
    public Transform sourceRoot;

    [Tooltip("Префаб слота, из которого берутся Variants. Например: OWall(2x1)Slot")]
    public GameObject slotPrefab;

    [Tooltip("Контейнер, куда складывать созданные слоты. Например: Slots/WallSlots/O_2x1")]
    public Transform targetRoot;
}

public class WallToSlotConverter : MonoBehaviour
{
    [SerializeField]
    private SlotMapping[] mappings;

    [ContextMenu("Generate Slots")]
    public void GenerateSlots()
    {
#if UNITY_EDITOR
        if (mappings == null || mappings.Length == 0)
        {
            Debug.LogWarning("Не задано ни одной связки (mappings).");
            return;
        }

        int totalCreated = 0;

        foreach (var mapping in mappings)
        {
            if (mapping.sourceRoot == null || mapping.slotPrefab == null || mapping.targetRoot == null)
            {
                Debug.LogWarning("Пропущена связка: не все поля заполнены.");
                continue;
            }

            int created = 0;
            for (int i = 0; i < mapping.sourceRoot.childCount; i++)
            {
                var wall = mapping.sourceRoot.GetChild(i);

                // Пропускаем всё, что не похоже на стену (без MeshRenderer).
                if (wall.GetComponent<MeshRenderer>() == null)
                    continue;

                var instance = (GameObject)PrefabUtility.InstantiatePrefab(mapping.slotPrefab);
                instance.name = wall.name + "_Slot";
                instance.transform.SetParent(mapping.targetRoot, true);
                instance.transform.position = wall.position;
                instance.transform.rotation = wall.rotation;
                instance.transform.localScale = wall.localScale;

                PrefabUtility.UnpackPrefabInstance(
                    instance,
                    PrefabUnpackMode.Completely,
                    InteractionMode.AutomatedAction);

                created++;
            }

            Debug.Log($"{mapping.sourceRoot.name} → создано слотов: {created}");
            totalCreated += created;
        }

        Debug.Log($"Всего создано слотов: {totalCreated}");
#else
        Debug.LogWarning("Генерация слотов доступна только в редакторе Unity.");
#endif
    }
}