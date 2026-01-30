using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    
    Vector2 moveInput;
    public bool IsMoving { get; private set; }
    public float speed = 5f;

    Rigidbody2D rg;

    private void Awake()
    {
        rg = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rg.linearVelocity = new Vector2(
            moveInput.x * speed,
            rg.linearVelocity.y
        );
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
    }
}
