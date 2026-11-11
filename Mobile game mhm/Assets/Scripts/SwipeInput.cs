using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class SwipeInput : MonoBehaviour
{
    private Vector2 startPosition;
    private Vector2 endPosition;
    private bool isTouching = false;
    private int trackedTouchId = -1;

    private float swipeThreshold = 50f;

    private void Awake() 
    {
        
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    private void BeginTouch(Vector2 position, int touchId)
    {
        isTouching = true;
        startPosition = position;
        trackedTouchId = touchId;
    }

    private void EndTouch(Vector2 position)
    {
        isTouching = false;
        endPosition = position;
        Vector2 swipeDirection = endPosition - startPosition;

        if (swipeDirection.magnitude > swipeThreshold)
        {
            swipeDirection = swipeDirection.normalized;
            Debug.Log(swipeDirection);
        }
        else
        {
            Debug.Log("Tap detected!");
        }

    }


    // Update is called once per frame
    void Update()
    {
        foreach (var touch in UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches)
        {
            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began && touch.screenPosition.x > Screen.width / 2f && !isTouching)
            {
                BeginTouch(touch.screenPosition, touch.touchId);
            }

            if (isTouching && touch.touchId == trackedTouchId)
            { 
                if (touch.phase == UnityEngine.InputSystem.TouchPhase.Ended || 
                    touch.phase == UnityEngine.InputSystem.TouchPhase.Canceled)
                {
                    EndTouch(touch.screenPosition);
                }
            }
        }
    }
}
