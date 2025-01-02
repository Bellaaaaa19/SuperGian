using UnityEngine;

public class PlayerHeart : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] GameObject[] heart;  // Array to hold heart GameObjects
    public int maxHealth = 3;    // Maximum health

    private int currentHealth;   // Current health of the enemy

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"Player takes {damage} damage!");

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health doesn't go below 0

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        for (int i = 0; i < heart.Length; i++)
        {
            heart[i].SetActive(i < currentHealth);
        }
    }

    void Die()
    {
        Debug.Log("Player has died!");
        Destroy(gameObject);
    }
}