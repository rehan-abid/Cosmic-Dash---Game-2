using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // --- Configuration (Set these in the Inspector) ---
    public GameObject[] enemyPrefabs;
    public Transform spawnPoint;
    public float minSpawnTime = 1f;
    public float maxSpawnTime = 3f;

    // NEW: Speed range configuration
    public float minEnemySpeed = 3f;
    public float maxEnemySpeed = 10f; // Max speed is inclusive for Random.Range(float, float)

    private float spawnTimer;

    void Start()
    {
        ResetSpawnTimer();
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            SpawnRandomEnemy();
            ResetSpawnTimer();
        }
    }

    void ResetSpawnTimer()
    {
        spawnTimer = Random.Range(minSpawnTime, maxSpawnTime);
    }

    void SpawnRandomEnemy()
    {
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemyToSpawn = enemyPrefabs[randomIndex];

        // 1. Instantiate the enemy
        GameObject newEnemy = Instantiate(enemyToSpawn, spawnPoint.position, Quaternion.identity);

        // 2. Calculate a random speed
        float randomSpeed = Random.Range(minEnemySpeed, maxEnemySpeed);

        // 3. Get the EnemyController component and set the speed
        EnemyController enemyController = newEnemy.GetComponent<EnemyController>();
        if (enemyController != null)
        {
            enemyController.SetSpeed(randomSpeed);
            Debug.Log($"Spawned enemy at speed: {randomSpeed:F2}");
        }
    }
}