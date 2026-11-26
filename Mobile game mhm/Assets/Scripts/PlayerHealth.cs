using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int maxLives = 3;  
    private int currentLives;
    private bool isInvincible = false; // Flag to check if the player is invincible
    private float invincibilityDuration = 2f; // Duration of invincibility in seconds
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        currentLives = maxLives;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerTakeDamage(int damage)
    {
        if (isInvincible) return;

        currentLives -= damage;
        isInvincible = true;
        Invoke(nameof(ResetInvincibility), invincibilityDuration);

        if (currentLives <= 0)
        {
            Debug.Log("Player has died.");
            // Handle player death (e.g., reload scene, show game over screen, etc.)
        }
    }
    public void ResetInvincibility()
    {
        isInvincible = false;
    }
}
