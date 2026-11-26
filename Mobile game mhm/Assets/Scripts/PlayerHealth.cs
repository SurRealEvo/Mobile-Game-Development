using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int maxLives = 3;  
    private int currentLives;
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
        currentLives -= damage;
        if (currentLives <= 0)
        {
            Debug.Log("Player has died.");
            // Handle player death (e.g., reload scene, show game over screen, etc.)
        }
    }
}
