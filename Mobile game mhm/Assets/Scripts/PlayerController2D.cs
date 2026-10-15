
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    public Camera mainCamera;

    private Vector3 offset;
    private float screenDepth;

    void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    void Start()
    {
        Debug.Log("PlayerController2D is running!");
    }

    void OnMouseDown()
    {
        Debug.Log("Player clicked!");
        screenDepth = mainCamera.WorldToScreenPoint(transform.position).z;

        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenDepth)
        );

        offset = transform.position - mouseWorldPosition;
    }

    void OnMouseDrag()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenDepth)
        );

        transform.position = mouseWorldPosition + offset;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse clicked at: " + Input.mousePosition);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if (hit.collider != null)
                Debug.Log("Hit object: " + hit.collider.gameObject.name);
            else
                Debug.Log("Nothing hit by camera ray.");
        }
    }

}
