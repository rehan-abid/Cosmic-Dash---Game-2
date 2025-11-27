using UnityEngine;
public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform spawnPoint;
    public float minSpawnTime = 2f;
    public float maxSpawnTime = 3.5f;
    public float minEnemySpeed = 5f;
    public float maxEnemySpeed = 12f;
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
        GameObject newEnemy = Instantiate(enemyToSpawn, spawnPoint.position, Quaternion.identity);
        float randomSpeed = Random.Range(minEnemySpeed, maxEnemySpeed);
        EnemyController enemyController = newEnemy.GetComponent<EnemyController>();
        if (enemyController != null)
        {
            enemyController.SetSpeed(randomSpeed);

            Debug.Log($"Spawned {enemyToSpawn.name} with speed {randomSpeed}");
        }
    }
}
