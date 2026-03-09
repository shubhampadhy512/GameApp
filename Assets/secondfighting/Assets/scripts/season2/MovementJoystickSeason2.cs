using UnityEngine;
using UnityEngine.EventSystems;

namespace Season2
{
    public class MovementJoystickSeason2 : MonoBehaviour
    {
        [Header("Player")]
        public PlayerController player;

        [Header("Joystick UI")]
        public RectTransform joystickBackground;
        public RectTransform joystickHandle;

        [Header("Settings")]
        public float moveRange = 100f;

        private Vector2 input;

        // Called from EventTrigger → PointerDown
        public void PointerDown(BaseEventData data)
        {
            Drag(data);
        }

        // Called from EventTrigger → Drag
        public void Drag(BaseEventData data)
        {
            PointerEventData eventData = (PointerEventData)data;

            Vector2 pos;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                joystickBackground,
                eventData.position,
                eventData.pressEventCamera,
                out pos
            );

            pos = Vector2.ClampMagnitude(pos, moveRange);

            joystickHandle.anchoredPosition = pos;

            input = pos / moveRange;

            if (player != null)
                player.SetMoveInput(new Vector2(input.x, 0));

            if (input.y > 0.6f && player != null)
                player.JumpButton();
        }

        // Called from EventTrigger → PointerUp
        public void PointerUp(BaseEventData data)
        {
            joystickHandle.anchoredPosition = Vector2.zero;

            input = Vector2.zero;

            if (player != null)
                player.SetMoveInput(Vector2.zero);
        }
    }
}