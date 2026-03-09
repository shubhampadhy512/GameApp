using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Mikemovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;

    private Rigidbody2D rb;
    private Vector2 keyboardInput;
    private Vector2 joystickInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        keyboardInput = Vector2.zero;

        // Keyboard input (new Input System)
        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
                keyboardInput.y = 1;

            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
                keyboardInput.y = -1;

            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
                keyboardInput.x = -1;

            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
                keyboardInput.x = 1;
        }
    }

    void FixedUpdate()
    {
        // Combine keyboard + joystick input
        Vector2 movement = keyboardInput + joystickInput;
        movement = Vector2.ClampMagnitude(movement, 1f);

        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    // Called by joystick
    public void SetMoveInput(Vector2 input)
    {
        joystickInput = input;
    }
}
