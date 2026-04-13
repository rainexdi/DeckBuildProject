using UnityEngine;

public enum AttackType { Melee, Ranged }
public enum MeleeVariation { Slash, Stab, Spin}
public enum RangedVariation { Arrow, Projectile, Fireball}

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Game/Enemy Stats")]
public class EnemyStatsSO : CharacterBaseSO
{


    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Combat")]
    public int attackDamage = 15;
    public float attackCooldown = 0.5f;
    public float attackRange = 1.5f;

    [Header("Attack Type")]
    public AttackType attackType;
    public MeleeVariation meleeVariation;
    public RangedVariation rangedVariation;

    [Header("Ranged Options")]
    public float projectileSpeed;
    public GameObject projectilePrefab;
}
