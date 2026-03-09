using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Damageable))]
public class Demogorgan : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 3f;
    public float walkStopRate = 0.6f;
    public DetectionZone attackZone;

    private Rigidbody2D rb;
    private Animator anim;
    private Damageable damageable;
    private Transform player;

    private bool _hasTarget = false;
    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            anim.SetBool("hasTarget", value);
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    private void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            player = p.transform;
    }

    private void Update()
    {
        // Stop everything if enemy is dead
        if (!damageable.IsAlive)
            return;

        // Stop if player does not exist
        if (player == null)
            return;

        Damageable playerDamageable = player.GetComponent<Damageable>();

        // 👇 STOP if player is dead
        if (playerDamageable == null || !playerDamageable.IsAlive)
        {
            HasTarget = false;
            anim.SetBool("isWalking", false);
            return;
        }

        // Check attack zone
        if (attackZone != null)
        {
            HasTarget = attackZone.detectedColliders.Count > 0;
        }

        // Flip towards player
        float directionX = player.position.x - transform.position.x;

        if (directionX > 0.1f && transform.localScale.x < 0)
        {
            Flip();
        }
        else if (directionX < -0.1f && transform.localScale.x > 0)
        {
            Flip();
        }

        anim.SetBool("isWalking", Mathf.Abs(rb.linearVelocity.x) > 0.1f);
    }

    private void FixedUpdate()
    {
        if (!damageable.IsAlive)
            return;

        if (damageable.LockVelocity)
            return;

        if (player == null)
            return;

        Damageable playerDamageable = player.GetComponent<Damageable>();

        // 👇 Only move if player is alive AND not in attack range
        if (playerDamageable != null && playerDamageable.IsAlive && !HasTarget)
        {
            float direction = Mathf.Sign(player.position.x - rb.position.x);
            rb.linearVelocity = new Vector2(direction * walkSpeed, rb.linearVelocity.y);
        }
        else
        {
            // Smooth deceleration
            rb.linearVelocity = new Vector2(
                Mathf.Lerp(rb.linearVelocity.x, 0, walkStopRate),
                rb.linearVelocity.y
            );
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // Called by Damageable Hit Event
    public void OnHit(int damage, Vector2 knockback)
    {
        if (!damageable.IsAlive)
            return;

        rb.linearVelocity = new Vector2(
            knockback.x,
            rb.linearVelocity.y + knockback.y
        );
    }
}