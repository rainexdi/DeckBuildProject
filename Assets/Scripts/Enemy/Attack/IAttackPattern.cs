using UnityEngine;

public interface IAttackPattern
{
    void Execute(Transform target);
    float GetAttackCD();
    void SetEnemyStats(EnemyStatsSO stats);
}
