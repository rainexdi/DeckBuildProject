using UnityEngine;
using Unity.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float cooldown;
    private float cooldownTimer;
    [SerializeField] private Transform[] spawnPoints;

    private void Update()
    {
        // Counter for the cooldown timer
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0)
        {
            // when it reaches 0, reset timer and spawn a enemy
            cooldownTimer = cooldown;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        // Get a random spawn point from the array.
        Transform target = spawnPoints[Random.Range(0, spawnPoints.Length)];
        // Spawn the enemy at the random spawn point 
        GameObject newObject = PoolManager.instance.GetObject(enemyPrefab, target);        
    }

}
