using UnityEngine;

namespace Season2
{
    public class Attack : MonoBehaviour
    {
        [Header("Attack Settings")]
        public int attackDamage = 10;
        public Vector2 knockback = new Vector2(5f, 2f);

        [Header("Target Settings")]
        public string targetTag = "Player";

        private Collider2D attackCollider;

        private void Awake()
        {
            attackCollider = GetComponent<Collider2D>();
            attackCollider.enabled = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag(targetTag))
                return;

            Damageable damageable = collision.GetComponentInParent<Damageable>();

            if (damageable != null && damageable.IsAlive)
            {
                Vector2 deliveredKnockback =
                    transform.root.localScale.x > 0
                    ? knockback
                    : new Vector2(-knockback.x, knockback.y);

                damageable.Hit(attackDamage, deliveredKnockback);
            }
        }
    }
}