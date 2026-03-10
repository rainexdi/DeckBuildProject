using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    void TakeDamage (int damage)
    {
        PlayerHealthManager.Instance.TakeDamage(damage);    
    }
}
