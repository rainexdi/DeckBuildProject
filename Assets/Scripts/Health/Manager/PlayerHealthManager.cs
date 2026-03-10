using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public static PlayerHealthManager Instance { get; private set; }

    private HealthComponent healthComponent;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        healthComponent = GetComponent<HealthComponent>();
    }

    public void TakeDamage(int damage) => healthComponent.TakeDamage(damage);
    public int CurrentHealth => healthComponent.CurrentHealth;
    public int MaxHealth => healthComponent.MaxHealth;
}


