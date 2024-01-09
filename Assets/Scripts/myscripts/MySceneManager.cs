using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    [SerializeField] private GameObject playerHolder; // Reference to the PlayerHolder object
    [SerializeField] private PlayerHealth playerHealth; // Reference to the PlayerHealth script
    [SerializeField] private CheckpointManager checkpointManager; // Reference to the CheckpointManager

    public static event Action OnCheckpointReload;

    // A public method to load a scene by its index
    public void LoadScene(int index)
    {
        // Check if the index is valid
        if (index >= 0 && index < SceneManager.sceneCountInBuildSettings)
        {
            // Load the scene asynchronously
            SceneManager.LoadSceneAsync(index);
        }
        else
        {
            // Print an error message
            Debug.LogError("Invalid scene index: " + index);
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        LoadScene(0);
    }

    // A public method to restart the current scene
    public void RestartScene()
    {
        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // Load the scene asynchronously
        SceneManager.LoadSceneAsync(currentSceneIndex);
    }

    public void ReloadFromCheckpoint()
    {
        // Trigger the event
        OnCheckpointReload?.Invoke();

        // Delegate respawn logic to CheckpointManager
        checkpointManager.RespawnPlayer();
    }
}
