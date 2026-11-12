using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{

    [UnitHeaderInspectable("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    
    [SerializeField] private FloatingJoystick joystick;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    private bool attackMovementOverride;
 
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput = Vector2.ClampMagnitude(
            joystick.Direction, 1f
            );
    }

    private void FixedUpdate()
    {
        if (attackMovementOverride)
            return;


        rb.linearVelocity = moveInput * moveSpeed;
    }

    public void SetAttackMovementOverride(bool isActive)
    {
        attackMovementOverride = isActive;
        rb.linearVelocity = Vector2.zero;
    }
}
