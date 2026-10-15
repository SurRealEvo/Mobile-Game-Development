
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2D : MonoBehaviour
{
    public Camera mainCamera;

    [Header("Movement Feel")]
    public float maxSpeed = 5f;
    public float weight = 0.25f;

    private Vector3 offset;
    private Vector3 velocity;
    private float screenDepth;
    private bool isDragging;

    void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    void Update()
    {
        if (Mouse.current == null || mainCamera == null)
            return;

        Vector2 mousePosition = Mouse.current.position.ReadValue();

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if (hit.collider != null &&
                hit.collider.gameObject == gameObject)
            {
                isDragging = true;

                screenDepth = mainCamera
                    .WorldToScreenPoint(transform.position).z;

                Vector3 mouseWorld = mainCamera.ScreenToWorldPoint(
                    new Vector3(mousePosition.x, mousePosition.y,
                                screenDepth));

                offset = transform.position - mouseWorld;
                velocity = Vector3.zero;
            }
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 mouseWorld = mainCamera.ScreenToWorldPoint(
                new Vector3(mousePosition.x, mousePosition.y,
                            screenDepth));

            Vector3 target = mouseWorld + offset;

            transform.position = Vector3.SmoothDamp(
                transform.position,
                target,
                ref velocity,
                weight,
                maxSpeed
            );
        }
    }
}
