using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Demogorgan : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 3f;
    public float walkStopRate = 0.05f; // How fast the enemy slides to a stop
    public DetectionZone attackZone;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    private Transform player;

    // Internal property to handle "hasTarget" boolean in the animator
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

    // NEW: Property to read "canMove" from the Animator
    public bool CanMove
    {
        get { return anim.GetBool("canMove"); }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }

    private void Update()
    {
        if (player == null) return;

        if (attackZone != null)
        {
            HasTarget = attackZone.detectedColliders.Count > 0;
        }

        float direction = player.position.x - transform.position.x;

        if (direction > 0)
            sr.flipX = false;
        else if (direction < 0)
            sr.flipX = true;

        anim.SetBool("isWalking", Mathf.Abs(direction) > 0.1f);
    }

    private void FixedUpdate()
    {
        if (player == null) return;

        // NEW: Check if the Animator allows movement
        if (CanMove)
        {
            float direction = player.position.x - rb.position.x;
            rb.linearVelocity = new Vector2(
                Mathf.Sign(direction) * walkSpeed,
                rb.linearVelocity.y
            );
        }
        else
        {
            // NEW: Smoothly slide to a stop if attacking/hit/dead
            rb.linearVelocity = new Vector2(
                Mathf.Lerp(rb.linearVelocity.x, 0, walkStopRate),
                rb.linearVelocity.y
            );
        }
    }
}