using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    private HealthComponent healthComponent;
    private void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
        healthComponent.OnDeath.AddListener(HandleDeath);
    }

    public void TakeDamage(int damage)
    {
        healthComponent.TakeDamage(damage);
    }
    private void HandleDeath()
    {
        // Add death logic here (e.g., play animation, drop loot, etc.)
        Destroy(gameObject);
    }


}
