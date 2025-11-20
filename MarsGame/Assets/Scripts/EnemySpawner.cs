using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // --- Configuration (Set these in the Inspector) ---

    // Array to hold the 5 different enemy prefabs.
    public GameObject[] enemyPrefabs;

    // An empty GameObject marking the fixed point where enemies will appear.
    public Transform spawnPoint;

    // Time range for how often enemies should spawn.
    public float minSpawnTime = 1f;
    public float maxSpawnTime = 3f;

    // --- Private Variables ---

    // Timer to track when the next enemy should appear.
    private float spawnTimer;

    void Start()
    {
        // Set the initial timer to a random value between the min and max.
        ResetSpawnTimer();
    }

    void Update()
    {
        // 1. Countdown the timer every frame.
        // Time.deltaTime is the time passed since the last frame.
        spawnTimer -= Time.deltaTime;

        // 2. Check if the timer has run out.
        if (spawnTimer <= 0)
        {
            // If time is up, spawn the enemy.
            SpawnRandomEnemy();

            // Immediately reset the timer for the next spawn event.
            ResetSpawnTimer();
        }
    }

    // Function to calculate and reset the timer.
    void ResetSpawnTimer()
    {
        // Pick a new random time between the configured range.
        spawnTimer = Random.Range(minSpawnTime, maxSpawnTime);
    }

    // Function to handle the actual enemy creation.
    void SpawnRandomEnemy()
    {
        // 3. Choose a random number from 0 up to (but not including) the total number of prefabs.
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemyToSpawn = enemyPrefabs[randomIndex];

        // 4. Create the enemy copy (instantiate) at the spawnPoint position.
        // Quaternion.identity means "no rotation."
        Instantiate(enemyToSpawn, spawnPoint.position, Quaternion.identity);
    }
}