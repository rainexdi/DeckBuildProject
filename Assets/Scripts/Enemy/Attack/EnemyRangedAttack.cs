using UnityEngine;

public abstract class EnemyRangedAttack : MonoBehaviour, IAttackPattern
{
    protected EnemyStatsSO enemyStats;
    protected Transform weaponTransform;
    protected Transform projectileSpawnPoint;
    protected float lastAttackTime;

    private void OnEnable()
    {
        weaponTransform = transform.Find("Weapon");
        projectileSpawnPoint = transform.Find("Weapon/spawnPoint");
    }
    public void SetEnemyStats(EnemyStatsSO stats)
    {
        enemyStats = stats;
    }

    public void Execute(Transform target)
    {
        if (Time.time < lastAttackTime + enemyStats.attackCooldown)
        {
            return;
        }

        float distance = Vector2.Distance(transform.position, target.position);
        if (distance <= enemyStats.attackRange)
        {
            if (weaponTransform != null)
            {
                AimHelper.AimAtTarget(weaponTransform, target.position);
            }
            ShootProjectile(target);
            lastAttackTime = Time.time;
        }
    }

    protected abstract void ShootProjectile(Transform target);
    public float GetAttackCD() => enemyStats.attackCooldown;
}

