using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    Vector2 moveInput;

    [Header("Movement Settings")]
    public float speed = 5f;
    public float jumpForce = 10f;

    [Header("Ground Check")]
    public ContactFilter2D groundFilter;
    private bool _isGrounded;

    [SerializeField]
    private bool _isMoving = false;
    public bool IsMoving
    {
        get => _isMoving;
        private set { _isMoving = value; animator.SetBool("isMoving", value); }
    }

    public bool _isFacingRight = true;
    public bool IsFacingRight
    {
        get => _isFacingRight;
        private set
        {
            if (_isFacingRight != value) transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            _isFacingRight = value;
        }
    }

    Animator animator;
    Rigidbody2D rg;

    private void Awake()
    {
        rg = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // This uses the "Ground Filter" you set in the Inspector!
        _isGrounded = rg.IsTouching(groundFilter);

        rg.linearVelocity = new Vector2(moveInput.x * speed, rg.linearVelocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
        SetFacingDirection(moveInput);
    }

    // THIS HANDLES SPACE AND PAGE UP
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && _isGrounded)
        {
            rg.linearVelocity = new Vector2(rg.linearVelocity.x, jumpForce);
            // animator.SetTrigger("jump"); // Uncomment if you have a jump animation
        }
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight) IsFacingRight = true;
        else if (moveInput.x < 0 && IsFacingRight) IsFacingRight = false;
    }
}