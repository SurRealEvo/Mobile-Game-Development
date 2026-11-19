using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
     private ScoreManager scoreManager;
    private int currentHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        currentHealth = maxHealth;
        scoreManager = FindFirstObjectByType<ScoreManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            TakeDamage(25);
            Debug.Log("Enemy health: " + currentHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;


        if (currentHealth <= 0)
        {
            scoreManager.IncreaseScore();
            Destroy(gameObject);
        }
    }
}
