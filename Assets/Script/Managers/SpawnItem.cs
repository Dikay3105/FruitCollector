using UnityEngine;

[System.Serializable]
public class SpawnItem
{
    public string nameLabel;
    public GameObject prefab;
    [Range(0f, 100f)]
    public float spawnWeight = 10f;
    public int scoreValue = 0;
    public int healthImpact = 0;
    public float minFallSpeed = 2f;
    public float maxFallSpeed = 5f;

    [Header("Fruit Sprites (Optional)")]
    public Sprite[] fruitSprites; // thêm đây
}
