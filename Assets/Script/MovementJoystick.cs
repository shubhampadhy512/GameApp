using UnityEngine;

public class MovementJoystick : MonoBehaviour
{
    public PlayerController player;
    public RectTransform joystickHandle;
    public float moveRange = 100f;

    private Vector2 startPos;
    private Vector2 input;

    void Start()
    {
        startPos = joystickHandle.anchoredPosition;
    }

    void Update()
    {
        Vector2 moveInput = new Vector2(input.x, 0);
        player.SetMoveInput(moveInput);
    }

    public void OnDrag(Vector2 dragPosition)
    {
        Vector2 offset = dragPosition - startPos;
        offset = Vector2.ClampMagnitude(offset, moveRange);

        joystickHandle.anchoredPosition = startPos + offset;

        input = offset / moveRange;
    }

    public void OnRelease()
    {
        joystickHandle.anchoredPosition = startPos;
        input = Vector2.zero;
    }
}