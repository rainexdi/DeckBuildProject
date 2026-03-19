using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    private HealthComponent healthComponent;
    public static UnityEvent OnEnemyDeath = new UnityEvent();
    private void Awake()
    {
        // Get the HealthComponent attached to the enemy and subscribe to the OnDeath event to handle enemy death logic
        healthComponent = GetComponent<HealthComponent>();
    }

    private void OnEnable()
    {
        // Subscribe to the OnDeath event when the enemy is enabled
        if (healthComponent != null)
        {
            healthComponent.OnDeath.AddListener(HandleDeath);
        }
    }

    private void OnDisable()
    {
        // Unsubscribe from the OnDeath event when the enemy is disabled
        if (healthComponent != null)
        {
            healthComponent.OnDeath.RemoveListener(HandleDeath);
        }
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
        OnEnemyDeath?.Invoke();
        ResetHealth();
    }


}
