using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("Attack Settings")]
    public int attackDamage = 10;
    public Vector2 knockback = new Vector2(5f, 2f);

    [Header("Target Settings")]
    [Tooltip("Which tag this attack should damage (e.g. 'Enemy' or 'Player')")]
    public string targetTag = "Enemy"; // default: player attacks enemies

    private Collider2D attackCollider;

    private void Awake()
    {
        attackCollider = GetComponent<Collider2D>();
        attackCollider.enabled = false; // disabled until attack starts
    }

    // Called from animation event when attack begins
    public void StartAttack()
    {
        attackCollider.enabled = true;
    }

    // Called from animation event when attack ends
    public void EndAttack()
    {
        attackCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit something: " + collision.name);

        // Only damage objects with the correct tag
        if (!collision.CompareTag(targetTag)) return;

        Damageable damageable = collision.GetComponentInParent<Damageable>();

        if (damageable != null && damageable.IsAlive)
        {
            // Flip knockback direction depending on facing
            Vector2 deliveredKnockback =
                transform.parent.localScale.x > 0
                ? knockback
                : new Vector2(-knockback.x, knockback.y);

            damageable.Hit(attackDamage, deliveredKnockback);
        }
    }
}
