using UnityEngine;

public class Enemy1 : MonoBehaviour
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
        Debug.Log($"Enemy takes {damage} damage!");

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
        Debug.Log("Enemy has died!");
        Destroy(gameObject);
    }
}
