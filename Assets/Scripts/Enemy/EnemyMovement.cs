using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private EnemyStatsSO enemyStats;
    private Transform playerTransform;
    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        MoveTowardsPlayer();
    }
    private void MoveTowardsPlayer()
    {
        // Moves the enemy towards the player's position. You can modify this to include pathfinding or other movement patterns if desired.
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        transform.position += direction * enemyStats.moveSpeed * Time.deltaTime;
    }

}
