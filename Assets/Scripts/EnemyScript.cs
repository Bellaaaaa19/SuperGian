using UnityEngine;

public class MoveRightToLeft : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement
    public float moveDistance = 10f; // Distance to move left and right
    private Vector3 startPosition; // Starting position of the object
    private bool movingRight = false; // Start by moving to the left
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
            if (transform.position.x >= startPosition.x)
            {
                isMoving = false; // Stop movement
            }
        }
        else
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

            // Check if the object has moved beyond the desired distance to the left
            if (transform.position.x <= startPosition.x - moveDistance)
            {
                movingRight = true; // Switch direction to right
            }
        }
    }
}
