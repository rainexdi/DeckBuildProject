using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    private HealthComponent healthComponent;
    private void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
        // Correct subscription: do not call HandleDeath(), add it as a listener.
        healthComponent.OnDeath.AddListener(HandleDeath);
    }

    public void TakeDamage(int damage)
    {
        healthComponent.TakeDamage(damage);
    }
    public void HandleDeath()
    {
        // Add death logic here (e.g., play animation, drop loot, etc.)
        Destroy(gameObject);
    }


}
