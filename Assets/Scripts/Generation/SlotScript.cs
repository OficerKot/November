using UnityEngine;

/*
Создает набор из заданных префабов и рандомно размещает один из него прямо в слот
*/
public class SlotScript : MonoBehaviour
{
    [SerializeField]
    private GameObject[] variants;

    public GameObject[] Variants => variants;

    public void CopyVariantsFrom(SlotScript other)
    {
        if (other == null)
            return;

        variants = other.variants;
    }

    [ContextMenu("Test Spawn Variant")]
    public void TestSpawnVariant()
    {
        if (variants == null || variants.Length == 0)
        {
            Debug.LogWarning("В слоте нет вариантов!");
            return;
        }

        int randomIndex = Random.Range(0, variants.Length);
        GameObject chosenPrefab = variants[randomIndex];

        Instantiate(chosenPrefab, transform.position, transform.rotation);
        Debug.Log($"Заспавнен вариант: {chosenPrefab.name}");
    }
}