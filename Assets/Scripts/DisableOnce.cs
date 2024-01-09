using UnityEngine;
using UnityEngine.SceneManagement;

public class DisableOnce : MonoBehaviour
{
    // Static variable to keep track of scene loads
    private static bool isFirstLoad = true;

    void OnEnable()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Unsubscribe from the sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if this is the first load
        if (isFirstLoad)
        {
            // Disable the GameObject and set isFirstLoad to false
            gameObject.SetActive(false);
            isFirstLoad = false;
        }
        else
        {
            // Ensure the GameObject is enabled on subsequent loads
            gameObject.SetActive(true);
        }
    }
}
