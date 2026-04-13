using UnityEngine;

public abstract class EnemyMeleeAttack : MonoBehaviour, IAttackPattern
{
    [SerializeField] private EnemyStatsSO enemyStats;
    private float lastAttackTime;

    public void Execute(Transform target)
    {
        if (Time.time < lastAttackTime + enemyStats.attackCooldown)
        {
            return; 
        }

        float distance = Vector2.Distance(transform.position, target.position);
        if (distance <= enemyStats.attackRange)
        {
            PerformAttack(target);
            lastAttackTime = Time.time;
        }
    }


    protected abstract void PerformAttack(Transform target);
    public float GetAttackCD() => enemyStats.attackCooldown;



}
