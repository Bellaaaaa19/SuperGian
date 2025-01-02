using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f; // Movement speed
    [SerializeField] float moveDistance = 5f; // Distance to move (set in Inspector)
    [SerializeField] int playerHealth = 3; // Player health
    [SerializeField] GameObject[] heart; // Heart UI array

    Animator animator;
    Rigidbody2D myRigidbody2D;
    Vector2 startPosition;
    bool isDead = false; // Track if the player is dead

    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        startPosition = transform.position; // Store the starting position
        playerHealth = heart.Length;
        animator.SetBool("Run", true); // Set running animation by default
    }

    void Update()
    {
        if (isDead)
        {
            // Stop all movement when dead
            animator.SetBool("Run", false); // Ensure the Run animation stops
            return;
        }

        MoveAutomatically();
    }



    void MoveAutomatically()
    {
        if (!isDead)
        {
            animator.SetBool("Run", true);
            Debug.Log("Run animation playing"); // Log for debugging
            myRigidbody2D.linearVelocity = new Vector2(-moveSpeed, myRigidbody2D.linearVelocity.y);
        }
        else
        {
            animator.SetBool("Run", false);
            Debug.Log("Run animation stopped"); // Log for debugging
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return; // If the player is already dead, don't process further

        playerHealth--;

        if (playerHealth > 0)
        {
            animator.SetBool("Death", false);
            StartCoroutine(ResetPosition()); // Call the coroutine here
        }
        else
        {
            HandleDeath();
        }

        // Destroy heart UI
        if (playerHealth >= 0 && playerHealth < heart.Length)
        {
            Destroy(heart[playerHealth].gameObject);
        }
    }

    IEnumerator ResetPosition()
    {
        moveSpeed = 0f; // Stop movement temporarily
        yield return new WaitForSeconds(0.5f); // Wait for half a second (optional)
        transform.position = startPosition + new Vector2(moveDistance, 0); // Reset player to start plus move distance (moving left)
        moveSpeed = 10f; // Resume movement
    }

    void HandleDeath()
    {
        isDead = true;
        moveSpeed = 0f; // Stop all movement
        myRigidbody2D.linearVelocity = Vector2.zero; // Stop Rigidbody motion
        animator.SetBool("Run", false); // Stop the Run animation
        animator.SetBool("Death", true); // Trigger the Death animation
        StartCoroutine(StopDeathAnimation()); // Stop Death after playing once
    }

    IEnumerator StopDeathAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // Wait for the animation to finish
        animator.SetBool("Death", false); // Reset the Death animation
    }




    IEnumerator GameOverSequence()
    {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds (play death animation)
        // You can add a Game Over screen or restart logic here
        Debug.Log("Game Over");
        // Example: Restart the level (if needed)
        // UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}