using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public void TakeDamage(int damage)
    {
        PlayerHealthManager.Instance.TakeDamage(damage);    
    }

    public void ResetHealth()
    {
        PlayerHealthManager.Instance.ResetHealth();
    }
}
