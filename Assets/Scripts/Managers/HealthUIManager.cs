using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events; 


public class HealthUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image healthBarFill;
    private HealthComponent healthComponent;
    

    private void Start()
    {
        PlayerHealthManager playerHealthManager = PlayerHealthManager.Instance;
        if (playerHealthManager != null)
        {
            healthComponent = playerHealthManager.healthComponent;
        }

        if (healthComponent != null)
        {
            UpdateHealthUI(healthComponent.CurrentHealth, healthComponent.MaxHealth);
        }

        if (healthComponent == null)
        {
            Debug.LogWarning("HealthComponent not found on PlayerHealthManager.");
        }
    }
    private void OnEnable()
    {
        if (healthComponent != null)
        {
            healthComponent.OnHealthChanged.AddListener(UpdateHealthUI); 
        }
    }

    private void OnDisable()
    {
        if (healthComponent != null)
        {
            healthComponent.OnHealthChanged.RemoveListener(UpdateHealthUI);
        }
    }

    private void UpdateHealthUI(int currentHealth, int maxHealth)
    {
        if (healthText != null)
        {
            healthText.text = $"HP: {currentHealth} / {maxHealth}";
        }

        if (healthBarFill != null && maxHealth > 0)
        {
            float fillAmount = (float)currentHealth / maxHealth;
            healthBarFill.fillAmount = fillAmount;
        }
    }
}
