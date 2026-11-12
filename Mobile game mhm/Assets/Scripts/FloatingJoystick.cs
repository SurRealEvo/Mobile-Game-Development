using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform joystickBackground;
    [SerializeField] private RectTransform joystickHandle;
    private RectTransform inputArea;

    private void Awake()
    {
        inputArea = GetComponent<RectTransform>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 localPoint;
        

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            inputArea,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint

            
        );

        joystickBackground.anchoredPosition = localPoint;
        joystickHandle.anchoredPosition = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickBackground,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint
        );

        joystickHandle.anchoredPosition = Vector2.ClampMagnitude(
        localPoint,
        joystickBackground.rect.width / 2
        );
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }
}