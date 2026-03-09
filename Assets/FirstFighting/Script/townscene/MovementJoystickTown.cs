using UnityEngine;
using UnityEngine.EventSystems;

public class MovementJoystickTown : MonoBehaviour
{
    [Header("Player Reference")]
    public Mikemovement player;

    [Header("Joystick UI")]
    public RectTransform joystickBackground;
    public RectTransform joystickHandle;

    [Header("Settings")]
    public float moveRange = 100f;

    private Vector2 input;

    // Called when pointer touches joystick
    public void PointerDown(BaseEventData data)
    {
    Debug.Log("down joystick");
        Drag(data);
    }

    // Called when dragging joystick
    public void Drag(BaseEventData data)
    {
    Debug.Log("Dragging joystick");
        PointerEventData eventData = (PointerEventData)data;

        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickBackground,
            eventData.position,
            eventData.pressEventCamera,
            out pos
        );

        // Clamp joystick movement
        pos = Vector2.ClampMagnitude(pos, moveRange);

        // Move handle
        joystickHandle.anchoredPosition = pos;

        // Normalize input
        input = pos / moveRange;

        // Send input to player
        if (player != null)
            player.SetMoveInput(input);
    }

    // Called when pointer is released
    public void PointerUp(BaseEventData data)
    {
    Debug.Log("up joystick");
        joystickHandle.anchoredPosition = Vector2.zero;
        input = Vector2.zero;

        if (player != null)
            player.SetMoveInput(Vector2.zero);
    }
}
