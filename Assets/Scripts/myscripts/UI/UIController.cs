using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1;
    }

    public void TogglePause()
    {
        if (Time.timeScale == 1)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }

    public void Level1()
    {
        SceneManager.LoadScene(1);
    }

    public void Level2()
    {
        SceneManager.LoadScene(2);
    }

    public void Level3()
    {
        return;
        //SceneManager.LoadScene(3);
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Time.timeScale = 1;
        Debug.Log("Quit");

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
