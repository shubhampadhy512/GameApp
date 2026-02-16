using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Demogorgan : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 3f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    private Transform player;

    private bool isFalling = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");

        if (p != null)
        {
            player = p.transform;
        }
        else
        {
            Debug.LogError("Player not found! Make sure Player has tag 'Player'");
        }
    }

    private void Update()
    {
        if (player == null || isFalling) return;

        float direction = player.position.x - transform.position.x;

        // Flip sprite based on direction
        if (direction > 0)
            sr.flipX = false;
        else if (direction < 0)
            sr.flipX = true;

        // Walking animation
        anim.SetBool("isWalking", Mathf.Abs(direction) > 0.1f);
    }

    private void FixedUpdate()
    {
        if (player == null || isFalling) return;

        float direction = player.position.x - rb.position.x;

        rb.linearVelocity = new Vector2(
            Mathf.Sign(direction) * walkSpeed,
            rb.linearVelocity.y
        );
    }

    // 🔥 Called when player hits enemy
    public void Fall()
    {
        if (isFalling) return;

        isFalling = true;

        // Stop movement
        rb.linearVelocity = Vector2.zero;

        // Stop walking animation
        anim.SetBool("isWalking", false);

        // Trigger fall animation
        anim.ResetTrigger("Falling");   // safety reset
        anim.SetTrigger("Falling");
    }
}
