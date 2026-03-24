using UnityEngine;

public class KillCounter : MonoBehaviour
{
    public int killCount = 0;
    private void OnEnable()
    {
        EnemyHealth.OnEnemyDeath.AddListener(onEnemyKilled);
        ResetKillCount();
    }

    private void OnDisable()
    {
        EnemyHealth.OnEnemyDeath.RemoveListener(onEnemyKilled);


    }

    public void onEnemyKilled()
    {
        killCount++;
    }

    public void ResetKillCount()
    {
        killCount = 0;
    }
}
