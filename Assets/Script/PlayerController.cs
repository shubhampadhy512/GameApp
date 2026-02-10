using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    // Components
    private Rigidbody2D rb;
    private Animator animator;

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
    private const string ATTACK_STATE = "attack_1"; // animation state name

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // Ground check
        isGrounded = rb.IsTouching(groundFilter);
        animator.SetBool(IS_GROUNDED, isGrounded);

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
        if (IsAttacking()) return;

        moveInput = context.ReadValue<Vector2>();
        isMoving = Mathf.Abs(moveInput.x) > 0.01f;

        if (moveInput.x > 0 && !isFacingRight)
            Flip();
        else if (moveInput.x < 0 && isFacingRight)
            Flip();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded && !IsAttacking())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (IsAttacking()) return;

        animator.ResetTrigger("Attack");
        animator.SetTrigger("Attack");
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
}
