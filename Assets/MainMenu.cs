using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // This method will be used to load a scene named "Level 1"
    public void PlayGame()
    {
        // Make sure the scene name is in quotes and matches the name of your scene exactly
        SceneManager.LoadScene("Level 0"); // Replace with your exact scene name if different
    }

    public void QuitGame()
    {
        Debug.Log("Quit");

#if UNITY_EDITOR
            // If we're in the editor, stop playing the scene instead of quitting
            UnityEditor.EditorApplication.isPlaying = false;
#else
        // Application.Quit will only work in a build
        Application.Quit();
#endif
    }
}
