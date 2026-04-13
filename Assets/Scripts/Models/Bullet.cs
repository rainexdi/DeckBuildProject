using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool _initialized = false;
    private int _damage;

    public void Intialize(float lifeTime, int damage)
    {
        _damage = damage;
        PoolManager.instance.ReturnObject(gameObject, lifeTime);
        _initialized = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(_damage);
            Debug.Log("Bullet hit " + other.gameObject.name + " for " + _damage + " damage.");
        }
        PoolManager.instance.ReturnObject(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(_damage);
            Debug.Log("Bullet hit " + other.gameObject.name + " for " + _damage + " damage.");
        }
        PoolManager.instance.ReturnObject(gameObject);

    }

    private void Start()
    {
        if (!_initialized)
        {
            PoolManager.instance.ReturnObject(gameObject, 2f);
        }
    }
}