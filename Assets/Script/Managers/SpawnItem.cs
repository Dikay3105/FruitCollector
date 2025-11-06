using UnityEngine;

[System.Serializable]
public class SpawnItem
{
    public string nameLabel;      // tên hiển thị (chỉ để nhìn Inspector)
    public GameObject prefab;     // prefab reference
    [Range(0f, 100f)]
    public float spawnWeight = 10f; // trọng số (càng lớn càng dễ ra)
    public int scoreValue = 0;      // điểm khi hứng (hoặc 0)
    public int healthImpact = 0;    // +1 để hồi, -1 để trừ
    public float minFallSpeed = 2f; // tốc độ rơi min
    public float maxFallSpeed = 5f; // tốc độ rơi max
}
