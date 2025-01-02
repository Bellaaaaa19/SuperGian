using System.Collections;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    [Header("Movement and Attack Settings")]
    public float moveSpeed = 2f; // Movement speed of the player
    public float attackRange = 1.5f; // Range at which the player will attack the enemy
    public int damage = 1; // Damage dealt to the enemy

    [Header("Health Settings")]
    [SerializeField] GameObject[] heart; // Array to hold heart GameObjects
    public int maxHealth = 3; // Maximum health

    private Transform enemy;
    private Vector3 startPosition;
    private bool isAttacking = false; // Prevents overlapping actions
    private Animator animator; // Reference to the Animator component
    private int currentHealth; // Current health of the player

    void Start()
    {
        // Initialize health
        currentHealth = maxHealth;
        UpdateHealthUI();

        // Find the enemy in the scene (ensure Enemy1 tag is set on the enemy)
        enemy = GameObject.FindGameObjectWithTag("Enemy1")?.transform;
        startPosition = transform.position;

        // Get the Animator component
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isAttacking) return; // Prevent movement while attacking
        if (enemy == null) return; // Ensure the enemy still exists

        if (Input.GetKeyDown(KeyCode.Y))
        {
            StartCoroutine(MoveAndAttackSequence());
        }
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

    private void Die()
    {
        Debug.Log("Player has died!");
        Destroy(gameObject);
    }

    void MoveTowardsEnemy()
    {
        Vector3 targetPosition = new Vector3(enemy.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        animator.SetBool("isMoving", true);
    }

    void StartAttack()
    {
        animator.SetBool("isMoving", false);
        animator.SetBool("isAttacking", true);
        StartCoroutine(AttackAndRetreat());
    }

    IEnumerator AttackAndRetreat()
    {
        isAttacking = true;

        // Wait for the attack animation to complete
        yield return new WaitForSeconds(0.5f);

        // Apply damage if the enemy is in range
        if (enemy != null && Vector3.Distance(transform.position, enemy.position) <= attackRange)
        {
            Enemy1 enemyScript = enemy.GetComponent<Enemy1>();
            if (enemyScript != null)
            {
                enemyScript.TakeDamage(damage);
            }
        }

        // Stop attack animation
        animator.SetBool("isAttacking", false);

        // Move back to starting position
        while (Vector3.Distance(transform.position, startPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        animator.SetBool("isMoving", false);
        isAttacking = false;
    }

    IEnumerator MoveAndAttackSequence()
    {
        while (Vector3.Distance(transform.position, enemy.position) > attackRange)
        {
            MoveTowardsEnemy();
            yield return null;
        }

        StartAttack();
    }
}
