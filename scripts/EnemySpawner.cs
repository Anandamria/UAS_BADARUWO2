using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 3f;
    public float minY = -4f, maxY = 4f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 2f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (enemyPrefab != null)
        {
            float randomY = Random.Range(minY, maxY);
            Vector3 spawnPos = new Vector3(transform.position.x, randomY, 0);
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Enemy prefab tidak terpasang di EnemySpawner!");
        }
    }
}
