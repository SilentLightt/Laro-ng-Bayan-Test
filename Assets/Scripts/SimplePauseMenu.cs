using UnityEngine;
using UnityEngine.SceneManagement;

public class SimplePauseMenu : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pauseMenuUI;  // The Pause Menu UI GameObject

    void Update()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();  // If paused, resume the game
            }
            else
            {
                Pause();  // If not paused, open the pause menu
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                Resume();  // If paused, resume the game
            }
            else
            {
                Pause();  // If not paused, open the pause menu
            }
        }
    }

    // Function to open the pause menu
    void Pause()
    {
        isPaused = true;
        pauseMenuUI.SetActive(true);  // Show the pause menu
        Time.timeScale = 0f;  // Freeze time (pause the game)
    }

    // Function to resume the game
    public void Resume()
    {
        isPaused = false;
        pauseMenuUI.SetActive(false);  // Hide the pause menu
        Time.timeScale = 1f;  // Resume time (unpause the game)
    }

    // Function to restart the current scene
    public void Restart()
    {
        Time.timeScale = 1f;  // Ensure the game is unpaused
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // Reload the current scene
    }

    // Function to quit the game
    public void Quit()
    {
        // Quit the game (only works in a built version, not in the editor)
        Application.Quit();
    }
}
