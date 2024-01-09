using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Public references to the GameObjects you want to control
    public GameObject firstGameObject;
    public GameObject secondGameObject;
    public GameObject thirdGameObject;

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
        if (isFirstLoad)
        {
            // Disable the GameObjects and set isFirstLoad to false
            if (firstGameObject != null) firstGameObject.SetActive(false);
            if (secondGameObject != null) secondGameObject.SetActive(false);
            if (thirdGameObject != null) thirdGameObject.SetActive(false);
            isFirstLoad = false;
        }
        else
        {
            // Enable the GameObjects on subsequent loads
            if (firstGameObject != null) firstGameObject.SetActive(true);
            if (secondGameObject != null) secondGameObject.SetActive(true);
            if (thirdGameObject != null) thirdGameObject.SetActive(true);
        }
    }
}
