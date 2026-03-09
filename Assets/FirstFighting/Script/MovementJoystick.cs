using UnityEngine;
using UnityEngine.EventSystems;

public class MovementJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public PlayerController player;

    public RectTransform joystickBackground;
    public RectTransform joystickHandle;

    public float moveRange = 100f;

    private Vector2 input;

    // When finger touches joystick
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    // When dragging joystick
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 touchPos = eventData.position;

        Vector2 offset = touchPos - (Vector2)joystickBackground.position;

        // Limit handle movement inside joystick
        offset = Vector2.ClampMagnitude(offset, moveRange);

        joystickHandle.anchoredPosition = offset;

        input = offset / moveRange;

        // Move player only left and right
        player.SetMoveInput(new Vector2(input.x, 0));

        // Jump if dragged upward
        if (input.y > 0.6f)
        {
            player.JumpButton();
        }
    }

    // When finger released
    public void OnPointerUp(PointerEventData eventData)
    {
        joystickHandle.anchoredPosition = Vector2.zero;

        input = Vector2.zero;

        // Stop movement
        player.SetMoveInput(Vector2.zero);
    }
}