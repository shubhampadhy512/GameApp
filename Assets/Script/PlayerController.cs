using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Damageable))]
public class PlayerController : MonoBehaviour
{
    // Components
    private Rigidbody2D rb;
    private Animator animator;
    private Damageable damageable;

    // Movement
    private Vector2 moveInput;

    [Header("Movement Settings")]
    public float speed = 5f;
    public float jumpForce = 7f;
    public float slideSpeed = 8f;
    public float slideDuration = 0.5f;

    // Ground check
    [Header("Ground Check")]
    public ContactFilter2D groundFilter;
    private bool isGrounded;

    // Facing
    private bool isFacingRight = true;
    private bool isMoving;

    // Sliding
    private bool isSliding;
    private float slideTimer;

    // Animator parameters
    private const string IS_MOVING = "isMoving";
    private const string IS_GROUNDED = "isGrounded";
    private const string ATTACK_TRIGGER = "Attack";
    private const string ATTACK_STATE = "attack_1";
    private const string IS_SLIDING = "isSliding";

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    private void FixedUpdate()
    {
        if (!damageable.IsAlive)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            animator.SetBool(IS_MOVING, false);
            return;
        }

        // Ground check
        isGrounded = rb.IsTouching(groundFilter);
        animator.SetBool(IS_GROUNDED, isGrounded);

        // Stop movement during knockback
        if (damageable.LockVelocity)
            return;

        // Handle sliding
        if (isSliding)
        {
            rb.linearVelocity = new Vector2((isFacingRight ? 1 : -1) * slideSpeed, rb.linearVelocity.y);
            slideTimer -= Time.fixedDeltaTime;
            if (slideTimer <= 0)
                StopSlide();
            return;
        }

        // Stop movement during attack
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
        if (!damageable.IsAlive) return;
        if (IsAttacking() || damageable.LockVelocity || isSliding) return;

        moveInput = context.ReadValue<Vector2>();
        isMoving = Mathf.Abs(moveInput.x) > 0.01f;

        if (moveInput.x > 0 && !isFacingRight)
            Flip();
        else if (moveInput.x < 0 && isFacingRight)
            Flip();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!damageable.IsAlive) return;

        if (context.performed && isGrounded && !IsAttacking() && !damageable.LockVelocity && !isSliding)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!damageable.IsAlive) return;
        if (!context.performed) return;
        if (IsAttacking() || damageable.LockVelocity || isSliding) return;

        animator.ResetTrigger(ATTACK_TRIGGER);
        animator.SetTrigger(ATTACK_TRIGGER);
    }

    public void OnSlide(InputAction.CallbackContext context)
    {
        if (!damageable.IsAlive) return;
        if (!context.performed) return;
        if (!isGrounded || IsAttacking() || damageable.LockVelocity || isSliding) return;

        StartSlide();
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

    // ================= SLIDE =================

    private void StartSlide()
    {
        isSliding = true;
        slideTimer = slideDuration;
        animator.SetBool(IS_SLIDING, true);
    }

    private void StopSlide()
    {
        isSliding = false;
        animator.SetBool(IS_SLIDING, false);
    }

    // ================= DAMAGE & KNOCKBACK =================

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.linearVelocity = new Vector2(knockback.x, rb.linearVelocity.y + knockback.y);

        if (knockback.x > 0 && isFacingRight)
            Flip();
        else if (knockback.x < 0 && !isFacingRight)
            Flip();
    }
}
