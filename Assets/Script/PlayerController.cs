using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Damageable))] // Added: Ensure Damageable exists
public class PlayerController : MonoBehaviour
{
    // Components
    private Rigidbody2D rb;
    private Animator animator;
    private Damageable damageable; // Added reference

    // Movement
    private Vector2 moveInput;
    [Header("Movement Settings")]
    public float speed = 5f;
    public float jumpForce = 7f;

    // Ground check
    [Header("Ground Check")]
    public ContactFilter2D groundFilter;
    private bool isGrounded;

    // Facing
    private bool isFacingRight = true;
    private bool isMoving;

    // Animator params
    private const string IS_MOVING = "isMoving";
    private const string IS_GROUNDED = "isGrounded";
    private const string ATTACK_TRIGGER = "Attack";
    private const string ATTACK_STATE = "attack_1";

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>(); // Initialize damageable
    }

    private void FixedUpdate()
    {
        // Ground check
        isGrounded = rb.IsTouching(groundFilter);
        animator.SetBool(IS_GROUNDED, isGrounded);

        // Changes: Check LockVelocity from Damageable script
        // If the character is being hit/knocked back, do not process move input
        if (damageable.LockVelocity)
        {
            return;
        }

        // If attack animation is playing → lock movement
        if (IsAttacking())
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            animator.SetBool(IS_MOVING, false);
            return;
        }

        // Normal movement
        rb.linearVelocity = new Vector2(moveInput.x * speed, rb.linearVelocity.y);
        animator.SetBool(IS_MOVING, isMoving);
    }

    // ================= INPUT =================

    public void OnMove(InputAction.CallbackContext context)
    {
        // Added: Prevent flipping or updating input during knockback/hit
        if (IsAttacking() || damageable.LockVelocity) return;

        moveInput = context.ReadValue<Vector2>();
        isMoving = Mathf.Abs(moveInput.x) > 0.01f;

        if (moveInput.x > 0 && !isFacingRight)
            Flip();
        else if (moveInput.x < 0 && isFacingRight)
            Flip();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // Added: Prevent jumping during hit
        if (context.performed && isGrounded && !IsAttacking() && !damageable.LockVelocity)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        // Added: Prevent attacking during hit
        if (IsAttacking() || damageable.LockVelocity) return;

        animator.ResetTrigger(ATTACK_TRIGGER);
        animator.SetTrigger(ATTACK_TRIGGER);
    }

    // ================= HELPERS =================

    private bool IsAttacking()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(ATTACK_STATE);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // ================= DAMAGE & KNOCKBACK =================

    public void OnHit(int damage, Vector2 knockback)
    {
        // The Damageable script calls this via Unity Event. 
        // We set the velocity immediately to the knockback vector.
        rb.linearVelocity = new Vector2(knockback.x, rb.linearVelocity.y + knockback.y);
    }
}