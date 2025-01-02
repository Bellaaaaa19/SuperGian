using UnityEngine;

public class Enemy1Health : MonoBehaviour
{
    public int maxHealth = 3; // Maximum health of the enemy
    private int currentHealth; // Current health

    public GameObject[] hearts; // Array of heart UI elements to represent health (optional)

    void Start()
    {
        // Initialize the current health to the maximum health
        currentHealth = maxHealth;
        Debug.Log($"Enemy1 initialized with health: {currentHealth}");
    }

    public void TakeDamage(int damage)
    {
        // Log to confirm the method is called
        Debug.Log("TakeDamage called on Enemy1!");

        // Decrease health
        currentHealth -= damage;
        Debug.Log($"Enemy1 current health: {currentHealth}");

        // Update hearts UI (if applicable)
        UpdateHeartsUI();

        // Check if health is 0 or below
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHeartsUI()
    {
        // Update heart UI based on health (if using hearts)
        if (hearts != null && hearts.Length > 0)
        {
            for (int i = 0; i < hearts.Length; i++)
            {
                hearts[i].SetActive(i < currentHealth);
            }
        }
    }

    private void Die()
    {
        Debug.Log("Enemy1 has died!");
        // Play death animation or effects here if needed
        Destroy(gameObject); // Destroy the enemy
    }
}
