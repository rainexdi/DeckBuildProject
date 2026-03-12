using UnityEngine;
using UnityEngine.InputSystem;
using CodeMonkey.Utils;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;

public class PlayerAttack : MonoBehaviour
{
    private Transform aimTransform;
    [SerializeField] private float shootForce = 10f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawnPoint;   
    [SerializeField] private float bulletLifeTime = 2f;
    [SerializeField] private PlayerStatsSO playerStats;

    private void Awake()
    {
        aimTransform = transform.Find("Aim");
    }
    private void Update()
    {
        HandleAim();
        Shoot();
    }

    private void HandleAim()
    {
        // Gets the mouse position in world space and calculates the direction from the player to the mouse. Then it rotates the aimTransform to point in that direction.
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - aimTransform.position).normalized;

        // Calculate the angle in degrees and set the rotation of the aimTransform
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }
    private void Shoot ()
    {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - aimTransform.position).normalized;

        if (!Mouse.current.leftButton.wasPressedThisFrame) return;
        else 
        {
            // Spawns the bullet and fires it in the direction of the aim.
            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            var runtimeBullet = bullet.AddComponent<Bullet>();
            rb.AddForce(aimDirection * shootForce, ForceMode2D.Impulse);
            runtimeBullet.Intialize(bulletLifeTime, playerStats.attackDamage);
        }
    } 
}

public class Bullet : MonoBehaviour
{
    private bool _initialized = false; 
    private int _damage;
    public void Intialize(float lifeTime, int damage)
    {
        _damage = damage;
        Destroy(gameObject, lifeTime);
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
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(_damage);
            Debug.Log("Bullet hit " + other.gameObject.name + " for " + _damage + " damage.");
        }
        Destroy(gameObject);

    }

    private void Start()
    {
        if (!_initialized)
        {
            Destroy(gameObject, 2f);
        }
    }
}
