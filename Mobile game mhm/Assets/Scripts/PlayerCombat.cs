using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    [SerializeField] private float dashSpeed = 24f;
    [SerializeField] private float dashDuration = 0.16f;

    private Rigidbody2D rb;
    private PlayerMovement playerMovement;


    private bool isAttacking;
    private float dashTimer;
    private Vector2 dashDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
        {
            StartDash(Vector2.right);
        }
    }

    private void FixedUpdate()
    {
        if (!isAttacking)
            return;

        rb.linearVelocity = dashDirection * dashSpeed;

        dashTimer -= Time.fixedDeltaTime;

        if (dashTimer <= 0f)
        {
            EndDash();
        }
    }

    public void TryStartDash(Vector2 direction)
    {
        if (isAttacking)
            return;

        StartDash(direction);
    }

    public void TryParry()
    {
        Debug.Log("parrryyyyyyyy");
    }
    private void StartDash(Vector2 direction)
    {
        isAttacking = true;
        dashTimer = dashDuration;
        dashDirection = direction.normalized;

        playerMovement.SetAttackMovementOverride(true);
    }

    private void EndDash()
    {
        isAttacking = false;
        rb.linearVelocity = Vector2.zero;

        playerMovement.SetAttackMovementOverride(false);
    }
}
