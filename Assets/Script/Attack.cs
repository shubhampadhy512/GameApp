using UnityEngine;

public class Attack : MonoBehaviour
{
    public int attackDamage = 10;
    public Vector2 knockback = new Vector2(5f, 2f);

    private Collider2D attackCollider;

    private void Awake()
    {
        attackCollider = GetComponent<Collider2D>();
        attackCollider.enabled = true; // VERY IMPORTANT
    }

    // Called from animation event when punch starts
    public void StartAttack()
    {
        attackCollider.enabled = true;
    }

    // Called from animation event when punch ends
    public void EndAttack()
    {
        attackCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit something: " + collision.name);
        Damageable damageable = collision.GetComponentInParent<Damageable>();

        if (damageable != null && damageable.IsAlive) // 👈 THIS IS IMPORTANT
        {
            Vector2 deliveredKnockback =
                transform.parent.localScale.x > 0
                ? knockback
                : new Vector2(-knockback.x, knockback.y);

            damageable.Hit(attackDamage, deliveredKnockback);
        }
    }
}