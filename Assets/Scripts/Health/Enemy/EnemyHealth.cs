using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    private HealthComponent healthComponent;
    private void Awake()
    {
        // Get the HealthComponent attached to the enemy and subscribe to the OnDeath event to handle enemy death logic
        healthComponent = GetComponent<HealthComponent>();
        healthComponent.OnDeath.AddListener(HandleDeath);
    }

    public void TakeDamage(int damage)
    {
        healthComponent.TakeDamage(damage);
    }

    public void ResetHealth()
    {
        healthComponent.ResetHealth(); 
    }
    public void HandleDeath()
    {
        // On Death we send the GameObject back to the pool and reset its health
        PoolManager.instance.ReturnObject(gameObject, 0);
        ResetHealth();
    }


}
