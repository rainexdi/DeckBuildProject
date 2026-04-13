using UnityEngine;

public class BulletAttack : EnemyRangedAttack
{
    private float bulletLifeTime = 2f;
    protected override void ShootProjectile(Transform target)
    {
        if (enemyStats == null) return;
        GameObject projectile = PoolManager.instance.GetObject(enemyStats.projectilePrefab, projectileSpawnPoint);

        Vector2 direction = AimHelper.GetAimDirection(weaponTransform.position, target.position);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        var runtimebullet = projectile.GetComponent<Bullet>();
        if (runtimebullet == null)
        {
            runtimebullet = projectile.AddComponent<Bullet>();
        }
        rb.linearVelocity = direction * enemyStats.projectileSpeed;
        runtimebullet.Intialize(bulletLifeTime, enemyStats.attackDamage);
    }

}
