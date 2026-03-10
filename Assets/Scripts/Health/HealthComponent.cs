using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private CharacterBaseSO charStats;

    public int CurrentHealth { get; private set; }
    public int MaxHealth { get; private set; }

    public UnityEvent<int, int> OnHealthChanged;
    public UnityEvent OnDeath;

    private void Awake()
    {
        MaxHealth = charStats.maxHealth;
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, MaxHealth);
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
        if (CurrentHealth <= 0) OnDeath?.Invoke();
    }
}