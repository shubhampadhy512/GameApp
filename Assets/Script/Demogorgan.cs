using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Damageable))]
public class Demogorgan : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 3f;
    public float walkStopRate = 0.6f; // Adjusted for Unity 6 linearVelocity
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
            if (anim != null) anim.SetBool("hasTarget", value);
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
        if (p != null) player = p.transform;
    }

    private void Update()
    {
        if (player == null || !damageable.IsAlive) return;

        // Check for player in attack zone
        if (attackZone != null)
        {
            HasTarget = attackZone.detectedColliders.Count > 0;
        }

        // Determine direction to player
        float directionX = player.position.x - transform.position.x;

        // Flip based on localScale so hitboxes rotate correctly
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
        // Stop movement if damageable is in a 'hit' or 'attacking' state
        if (damageable.LockVelocity) return;

        if (player != null && !HasTarget)
        {
            float direction = Mathf.Sign(player.position.x - rb.position.x);
            rb.linearVelocity = new Vector2(direction * walkSpeed, rb.linearVelocity.y);
        }
        else
        {
            // Smoothly decelerate to zero
            rb.linearVelocity = new Vector2(Mathf.Lerp(rb.linearVelocity.x, 0, walkStopRate), rb.linearVelocity.y);
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // This is called by the Damageable Hit Event in the Inspector
    public void OnHit(int damage, Vector2 knockback)
    {
        rb.linearVelocity = new Vector2(knockback.x, rb.linearVelocity.y + knockback.y);
    }
}