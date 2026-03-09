using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 moveSpeed = new Vector2(7f, 0);
    public int damage = 10;
    public Vector2 knockback = new Vector2(0, 0);

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // Set velocity based on speed and the direction the projectile is facing
        rb.linearVelocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            // Apply damage and knockback
            bool gotHit = damageable.Hit(damage, knockback);

            if (gotHit)
            {
                Debug.Log(collision.name + " hit for " + damage);
                Destroy(gameObject); // Destroy arrow on impact
            }
        }
    }
}