using UnityEngine;

public class MoveLeftToRight : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement
    public float moveDistance = 10f; // Distance to move left and right
    private Vector3 startPosition; // Starting position of the object
    private bool movingRight = true; // Whether the object is moving to the right or left
    private bool isMoving = true; // Whether the object should move

    void Start()
    {
        // Save the starting position
        startPosition = transform.position;
    }

    void Update()
    {
        if (!isMoving) return; // Exit if movement is stopped

        // Move the object left or right
        if (movingRight)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

            // Check if the object has moved beyond the desired distance to the right
            if (transform.position.x >= startPosition.x + moveDistance)
            {
                movingRight = false; // Switch direction to left
            }
        }
        else
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

            // Check if the object has returned to the starting position
            if (transform.position.x <= startPosition.x)
            {
                isMoving = false; // Stop movement
            }
        }
    }
}