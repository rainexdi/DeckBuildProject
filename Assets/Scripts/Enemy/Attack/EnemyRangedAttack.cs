using UnityEngine;

public abstract class EnemyRangedAttack : MonoBehaviour, IAttackPattern
{
    [SerializeField] protected EnemyStatsSO enemyStats;
    [SerializeField] protected Transform weaponTransform;
    [SerializeField] protected Transform projectileSpawnPoint;
    protected float lastAttackTime;

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

