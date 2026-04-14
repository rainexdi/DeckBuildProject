using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyStatsSO enemyStats;

    private IAttackPattern attackPattern;
    private EnemyMovement movement;
    private Transform player;
    private SpriteRenderer spriteRenderer;

    private void OnEnable()
    {
        movement = GetComponent<EnemyMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindFirstObjectByType<PlayerMovement>().transform;

        if (enemyStats != null)
        {
            gameObject.name = enemyStats.characterName;
            // Initialize attack pattern based on enemy type
        }
    }

    private void SpawnAttackPattern()
    {
        //
        if (TryGetComponent<IAttackPattern>(out var existingAttack))
        {
            Destroy(existingAttack as MonoBehaviour);
        }

        MonoBehaviour attackComponent = enemyStats.attackType switch
        {
            
            AttackType.Melee => SpawnMeleeAttack(),
            AttackType.Ranged => SpawnRangedAttack(),
            _ => null
        };

        if (attackComponent is IAttackPattern attack)
        {
            attack.SetEnemyStats(enemyStats);
            attackPattern = attack;
        }
    }

    private MonoBehaviour SpawnMeleeAttack()
    {
        return enemyStats.meleeVariation switch
        {
            /*
            MeleeVariation.Slash => gameObject.AddComponent<SlashAttack>(),
            MeleeVariation.Stab => gameObject.AddComponent<StabAttack>(),
            _ => gameObject.AddComponent<SlashAttack>() // Default to SlashAttack if no specific variation is set
            */
        };
    }

    private MonoBehaviour SpawnRangedAttack()
    {
        return enemyStats.rangedVariation switch
        {
            /*
            RangedVariation.Shoot => gameObject.AddComponent<BulletAttack>(),
            RangedVariation.Throw => gameObject.AddComponent<ThrowAttack>(),
            _ => gameObject.AddComponent<BulletAttack>() // Default to BulletAttack if no specific variation is set
            */
        };
    }

    private void Update()
    {
        attackPattern?.Execute(player);
        movement?.MoveTowardsPlayer();
    }
}
