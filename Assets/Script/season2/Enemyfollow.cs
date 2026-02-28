using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;
    public float attackDistance = 0.8f;

    private Rigidbody2D rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float distance = Mathf.Abs(player.position.x - transform.position.x);

        // Move toward player
        if (distance > attackDistance)
        {
            float direction = Mathf.Sign(player.position.x - transform.position.x);

            rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);

            anim.SetBool("isRunning", true);
        }
        else
        {
            // Stop and attack
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

            anim.SetBool("isRunning", false);
            anim.SetTrigger("attack");
        }

        // Flip enemy
        if (player.position.x < transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
    }
}