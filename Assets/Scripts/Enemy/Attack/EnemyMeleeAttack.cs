using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public abstract class EnemyMeleeAttack : MonoBehaviour, IAttackPattern
{
    protected EnemyStatsSO enemyStats;
    protected Transform weaponTransform;
    private float lastAttackTime;

    private void OnEnable()
    {
        weaponTransform = transform.Find("Weapon");
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
            PerformAttack(target);
            lastAttackTime = Time.time;
        }
    }


    protected abstract void PerformAttack(Transform target);
    public float GetAttackCD() => enemyStats.attackCooldown;



}
