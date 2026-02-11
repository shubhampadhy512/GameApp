using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Demogorgan : MonoBehaviour
{
    public float walkSpeed = 3f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    private Transform player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        // Automatically find player
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
        if (player == null) return;

        float direction = player.position.x - transform.position.x;

        // Flip sprite
        if (direction > 0)
            sr.flipX = false;
        else if (direction < 0)
            sr.flipX = true;

        // Walking animation
        anim.SetBool("isWalking", Mathf.Abs(direction) > 0.1f);
    }

    private void FixedUpdate()
    {
        if (player == null) return;

        float direction = player.position.x - rb.position.x;

        rb.linearVelocity = new Vector2(
            Mathf.Sign(direction) * walkSpeed,
            rb.linearVelocity.y
        );
    }
}
