// using UnityEngine;

// public class Attack : MonoBehaviour
// {
//     [Header("Attack Settings")]
//     public int attackDamage = 10;
//     public Vector2 knockback = new Vector2(5f, 2f);

//     [Header("Target Settings")]
//     [Tooltip("Which tag this attack should damage (e.g. 'Enemy' or 'Player')")]
//     public string targetTag = "Enemy"; // default: player attacks enemies

//     private Collider2D attackCollider;

//     private void Awake()
//     {
//         attackCollider = GetComponent<Collider2D>();
//         attackCollider.enabled = false; // disabled until attack starts
//     }

//     // Called from animation event when attack begins
//     public void StartAttack()
//     {
//         attackCollider.enabled = true;
//     }

//     // Called from animation event when attack ends
//     public void EndAttack()
//     {
//         attackCollider.enabled = false;
//     }

//     private void OnTriggerEnter2D(Collider2D collision)
//     {
//         // Only damage objects with the correct tag
//         if (!collision.CompareTag(targetTag)) return;

//         Damageable damageable = collision.GetComponentInParent<Damageable>();

//         if (damageable != null && damageable.IsAlive)
//         {
//             // Flip knockback direction depending on facing
//             Vector2 deliveredKnockback =
//                 transform.parent.localScale.x > 0
//                 ? knockback
//                 : new Vector2(-knockback.x, knockback.y);

//             damageable.Hit(attackDamage, deliveredKnockback);
//         }
//     }
// }
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("Attack Settings")]
    public int attackDamage = 10;
    public Vector2 knockback = new Vector2(5f, 2f);

    [Header("Cooldown")]
    public float attackCooldown = 0.5f;
    private float lastAttackTime;

    [Header("Target Settings")]
    public string targetTag = "Enemy";

    private Collider2D attackCollider;

    private PlayerAudioManager playerAudio;
    private EnemyAudioManager enemyAudio;

    private void Awake()
    {
        attackCollider = GetComponent<Collider2D>();
        attackCollider.enabled = false;

        playerAudio = GetComponentInParent<PlayerAudioManager>();
        enemyAudio = GetComponentInParent<EnemyAudioManager>();
    }

    public void StartAttack()
    {
        if (Time.time < lastAttackTime + attackCooldown)
            return;

        lastAttackTime = Time.time;

        attackCollider.enabled = true;

        if (playerAudio != null)
            playerAudio.PlayAttack();

        if (enemyAudio != null)
            enemyAudio.PlayAttack();
    }

    public void EndAttack()
    {
        attackCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(targetTag)) return;

        Damageable damageable = collision.GetComponentInParent<Damageable>();

        if (damageable != null && damageable.IsAlive)
        {
            Vector2 deliveredKnockback =
                transform.parent.localScale.x > 0
                ? knockback
                : new Vector2(-knockback.x, knockback.y);

            damageable.Hit(attackDamage, deliveredKnockback);
        }
    }
}