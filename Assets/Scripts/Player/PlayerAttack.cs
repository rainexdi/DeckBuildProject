using UnityEngine;
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
    public Vector2 PointerPosition { get; set; }

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
        AimHelper.AimAtTarget(aimTransform, PointerPosition);
    }
    public void Shoot ()
    {
        Vector3 aimDirection = AimHelper.GetAimDirection(aimTransform.position, PointerPosition);

        if (!Mouse.current.leftButton.wasPressedThisFrame) return;
        else 
        {
            // Spawns the bullet and fires it in the direction of the aim.
            GameObject bullet = PoolManager.instance.GetObject(bulletPrefab, spawnPoint);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            var runtimeBullet = bullet.GetComponent<Bullet>();
            if (runtimeBullet == null)
            {
                runtimeBullet = bullet.AddComponent<Bullet>();  
            }
            rb.linearVelocity = aimDirection * shootForce;
            runtimeBullet.Intialize(bulletLifeTime, playerStats.attackDamage);
        }
    } 
}


