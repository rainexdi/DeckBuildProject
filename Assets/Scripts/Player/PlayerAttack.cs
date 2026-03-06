using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private float shootForce = 10f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform Spawn;   
    [SerializeField] private float bulletLifeTime = 2f;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            Shoot();
        }
    }
    public void Shoot ()
    {
        GameObject bullet = Instantiate(bulletPrefab, Spawn.position, Quaternion.identity);
        Vector2 shootDirection = playerMovement.movementInput; 
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(shootDirection * shootForce, ForceMode2D.Impulse);  
        Destroy(bullet, bulletLifeTime);
    }
}
