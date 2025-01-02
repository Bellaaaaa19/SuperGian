using UnityEngine;
using UnityEngine.SceneManagement;

public class Levelmover : MonoBehaviour
{
    // Variable to set the scene index from the Inspector
    public int sceneBuildIndex;

    // Trigger when something enters the collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the other collider is not null and has the "Player" tag
        if (other != null && other.CompareTag("Player"))
        {
            // Log and switch scenes
            print("Switching Scene to " + sceneBuildIndex);
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }
        else
        {
            print("Object not tagged as Player, no scene change.");
        }
    }
}
