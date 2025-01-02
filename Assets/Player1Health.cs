using UnityEngine;

public class Player1Health : MonoBehaviour
{
    public int health = 3; // Health of the player

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Player1 took damage. Remaining health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player1 has died!");
        Destroy(gameObject); // Remove the player from the scene
    }
}
