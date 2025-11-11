using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public Text scoreText;
    public Text healthText;
    public Text levelText;
    public GameObject gameOverPanel;
    public PlayerController player;

    [Header("Spawn Settings")]
    public float minSpawnInterval = 0.7f;
    public SpawnItem[] spawnItems;
    public float spawnInterval = 1.2f;
    public float spawnYOffset = 1f; // spawn cao hơn top camera bao nhiêu
    public int startingHealth = 3;
    public float difficultyMultiplierPerLevel = 0.9f; // spawnInterval *= multiplier to speed up

    private int score = 0;
    private int health;
    private int level = 1;
    private float totalWeight;
    private float minX, maxX;
    private float topY;

    void Start()
    {
        health = startingHealth;
        ComputeScreenBounds();
        ComputeWeightedItemsForLevel(); // tính weight theo level
        UpdateUI();
        InvokeRepeating(nameof(SpawnObject), 1f, spawnInterval);
    }

    void ComputeScreenBounds()
    {
        Vector3 right = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
        minX = -right.x + 0.5f; // padding 0.5
        maxX = right.x - 0.5f;
        topY = Camera.main.transform.position.y + Camera.main.orthographicSize;
    }

    // Tính tổng weight dựa theo level
    void ComputeWeightedItemsForLevel()
    {
        totalWeight = 0f;
        foreach (var item in spawnItems)
        {
            bool isFruit = item.nameLabel.ToLower().Contains("fruit");
            if (!isFruit)
            {
                // tăng spawnWeight bom/gear theo level (ví dụ +10% mỗi level)
                item.spawnWeight *= 1f + (level - 1) * 0.1f;
            }
            totalWeight += Mathf.Max(0f, item.spawnWeight);
        }
        if (totalWeight == 0f) totalWeight = 1f;
    }

    void SpawnObject()
    {
        SpawnItem selected = GetRandomItemWeighted();
        if (selected == null || selected.prefab == null) return;

        float x = Random.Range(minX, maxX);
        float y = topY + spawnYOffset;
        GameObject obj = Instantiate(selected.prefab, new Vector3(x, y, 0f), Quaternion.identity);

        // gán Order in Layer = 2
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr != null) sr.sortingOrder = 2;

        // gán tốc độ rơi
        FallingObject fo = obj.GetComponent<FallingObject>();
        if (fo == null) fo = obj.AddComponent<FallingObject>();

        bool isFruit = selected.nameLabel.ToLower().Contains("fruit");
        bool isBombOrGear = !isFruit;

        if (isFruit)
        {
            // Trái cây tốc độ bình thường
            fo.fallSpeed = Random.Range(selected.minFallSpeed, selected.maxFallSpeed);
        }
        else if (isBombOrGear)
        {
            // Bom/gear tăng tốc theo level
            float speedMultiplier = 1f + (level - 1) * 0.1f;
            fo.fallSpeed = Random.Range(selected.minFallSpeed, selected.maxFallSpeed) * speedMultiplier;
        }

        // gắn PickupData để Basket có thể đọc
        PickupData pd = obj.GetComponent<PickupData>();
        if (pd == null) pd = obj.AddComponent<PickupData>();
        pd.Setup(selected.scoreValue, selected.healthImpact);
    }

    SpawnItem GetRandomItemWeighted()
    {
        float r = Random.value * totalWeight;
        float sum = 0f;
        foreach (var item in spawnItems)
        {
            sum += Mathf.Max(0f, item.spawnWeight);
            if (r <= sum) return item;
        }
        return spawnItems.Length > 0 ? spawnItems[0] : null;
    }

    public void AddScore(int amount)
    {
        score += amount;
        if (score < 0) score = 0;

        int requiredScore = level * 20; // mỗi level cần 20 điểm
        if (score >= requiredScore)
        {
            LevelUp(level + 1);
        }

        UpdateUI();
    }

    public void ChangeHealth(int delta)
    {
        health += delta;
        UpdateUI();
        if (health <= 0) GameOver();
    }

    void LevelUp(int newLevel)
    {
        level = newLevel;

        // Tăng tỉ lệ bom/gear
        ComputeWeightedItemsForLevel();

        // Giảm spawnInterval nhưng không thấp hơn giới hạn
        spawnInterval *= difficultyMultiplierPerLevel;
        spawnInterval = Mathf.Max(spawnInterval, minSpawnInterval);

        // Tăng tốc độ người chơi
        player.IncreaseSpeed(1.5f);

        // Restart spawn timer
        CancelInvoke(nameof(SpawnObject));
        InvokeRepeating(nameof(SpawnObject), 0.5f, spawnInterval);
    }

    void GameOver()
    {
        CancelInvoke(nameof(SpawnObject));
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
    }

    void UpdateUI()
    {
        if (scoreText) scoreText.text = "Score: " + score;
        if (healthText) healthText.text = "Health: " + health;
        if (levelText) levelText.text = "Level: " + level;
    }
}

