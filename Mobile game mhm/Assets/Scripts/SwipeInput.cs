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

    private PlayerCombat playerCombat;

    private void Awake() 
    {
        playerCombat = GetComponent<PlayerCombat>();
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
        if (position.x <= Screen.width / 2f)
            return;

        isTouching = true;
        startPosition = position;
        trackedTouchId = touchId;
        
    }

    private void EndTouch(Vector2 position)
    {
        
        endPosition = position;
        Vector2 swipeDirection = endPosition - startPosition;
       

        if (swipeDirection.magnitude > swipeThreshold)
        {
            swipeDirection = swipeDirection.normalized;
            playerCombat.TryStartDash(swipeDirection);
            Debug.Log("Swipe detected: " + swipeDirection);
        }
        else
        {
            playerCombat.TryParry();
        }

        isTouching = false;
        trackedTouchId = -1;
    }


    // Update is called once per frame
    private void Update()
    {
        
        foreach (Touch touch in Touch.activeTouches)
        {
            
            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began && touch.screenPosition.x > Screen.width / 2f && !isTouching)
            {
                BeginTouch(touch.startScreenPosition, touch.touchId);
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
