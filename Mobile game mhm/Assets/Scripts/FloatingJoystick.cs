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
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }
}