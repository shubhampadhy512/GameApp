using UnityEngine;
using UnityEngine.EventSystems; // Required for touch events

public class MovementJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public PlayerController player;
    public RectTransform joystickBackground;
    public RectTransform joystickHandle;
    
    [Header("Settings")]
    public float moveRange = 100f;

    private Vector2 startPos;
    private Vector2 input;

    void Start()
    {
        // Store the initial position of the background
        startPos = joystickBackground.anchoredPosition;
    }

    // Called when the user first touches the screen/panel
    public void OnPointerDown(PointerEventData eventData)
    {
        // Optional: Move the joystick to the touch position (Video Style)
        joystickBackground.position = eventData.position;
        OnDrag(eventData);
    }

    // Called while the user is dragging
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 touchPos = eventData.position;
        Vector2 offset = touchPos - (Vector2)joystickBackground.position;

        // Clamp the handle within the move range
        offset = Vector2.ClampMagnitude(offset, moveRange);
        joystickHandle.anchoredPosition = offset;

        // Calculate input (-1 to 1)
        input = offset / moveRange;

        // Send X-axis only to the side-view player controller
        player.SetMoveInput(new Vector2(input.x, 0));
    }

    // Called when the user lifts their finger
    public void OnPointerUp(PointerEventData eventData)
    {
        // Reset joystick visuals
        joystickBackground.anchoredPosition = startPos;
        joystickHandle.anchoredPosition = Vector2.zero;

        // Stop the player
        input = Vector2.zero;
        player.SetMoveInput(Vector2.zero);
    }
}