using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivateOnce : MonoBehaviour
{
    private static bool isActivatedOnce = false;

    void Start()
    {
        // Activate the object only if it wasn't activated before
        if (!isActivatedOnce)
        {
            gameObject.SetActive(true);
            isActivatedOnce = true;
        }
        else
        {
            gameObject.SetActive(false);
        }

        // Register for the scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // Unregister from the scene loaded event when this object is being destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Deactivate the object when a new scene is loaded
        gameObject.SetActive(false);
    }
}
