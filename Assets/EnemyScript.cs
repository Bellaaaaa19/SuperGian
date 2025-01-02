using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float moveSpeed = 2f; // Movement speed of the enemy
    public float attackRange = 1.5f; // Range at which the enemy will attack the player
    public int damage = 1; // Damage dealt to the player

    private Transform player;
    private Vector3 startPosition;
    private bool isAttacking = false; // Prevents overlapping actions
    private Animator animator; // Reference to the Animator component

    void Start()
    {
        // Find the player in the scene (ensure Player1 tag is set on the player)
        player = GameObject.FindGameObjectWithTag("Player1")?.transform;
        startPosition = transform.position;

        // Get the Animator component
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isAttacking) return; // Prevent movement while attacking
        if (player == null) return; // Ensure the player still exists

        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(MoveAndAttackSequence());
        }
    }

    private void Die()
    {
        Debug.Log("Enemy has died!");
        Destroy(gameObject);
    }

    void MoveTowardsPlayer()
    {
        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        animator.SetBool("Moving", true);
    }

    void StartAttack()
    {
        animator.SetBool("Moving", false);
        animator.SetBool("Attacking", true);
        StartCoroutine(AttackAndRetreat());
    }

    IEnumerator AttackAndRetreat()
    {
        isAttacking = true;

        // Wait for the attack animation to complete
        yield return new WaitForSeconds(0.5f);

        // Apply damage if the player is in range
        if (player != null && Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            Player1 playerScript = player.GetComponent<Player1>();
            if (playerScript != null)
            {
                playerScript.TakeDamage(damage);
            }
        }

        // Stop attack animation
        animator.SetBool("Attacking", false);

        // Move back to starting position
        while (Vector3.Distance(transform.position, startPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        animator.SetBool("Moving", false);
        isAttacking = false;
    }

    IEnumerator MoveAndAttackSequence()
    {
        while (Vector3.Distance(transform.position, player.position) > attackRange)
        {
            MoveTowardsPlayer();
            yield return null;
        }

        StartAttack();
    }
}
