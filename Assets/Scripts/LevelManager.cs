using System.Timers;
using System.Threading;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Include TextMeshPro namespace

public class LevelManager : MonoBehaviour 
{
    public Canvas levelCompletedCanvas;
    public Timer timerScript;
    public TextMeshProUGUI timeDisplay;
    public PlayerHealth playerHealth;
    public TextMeshProUGUI healthDisplay;
    public StarScorer starScorer;
    public UIDisabler uiDisabler;
    public ObjectiveTracker objectiveTracker;
    public ItemCollectionTracker itemCollectionTracker;

    public GameplayLocalizationManager localizationManager; // Reference to the Localization Manager

    void Start()
    {
        levelCompletedCanvas.gameObject.SetActive(false);
        objectiveTracker.OnObjectiveCompleted += HandleLevelComplete;
        //itemCollectionTracker.OnObjectiveCompleted += HandleLevelComplete;
    }

    void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks
        objectiveTracker.OnObjectiveCompleted -= HandleLevelComplete;
    }

    public void HandleLevelComplete()
    {
        levelCompletedCanvas.gameObject.SetActive(true);
        DisplayElapsedTime();  // Display elapsed time
        DisplayRemainingHealth();  // Display remaining health
        uiDisabler.DisableAllUI();  // Use UIDisabler to disable UI
        starScorer.LevelCompleted();

        //Time.timeScale = 0;
    }

    private void DisplayElapsedTime()
    {
        float elapsed = timerScript.elapsedTime;
        int minutes = Mathf.FloorToInt(elapsed / 60);
        int seconds = Mathf.FloorToInt(elapsed % 60);

        // Check current language and display time accordingly
        if (localizationManager.CurrentLanguage == Language.English)
        {
            timeDisplay.text = string.Format("Time Elapsed: {0:00}:{1:00}", minutes, seconds);
        }
        else if (localizationManager.CurrentLanguage == Language.Indonesian)
        {
            timeDisplay.text = string.Format("Waktu Berlalu: {0:00}:{1:00}", minutes, seconds);
        }
    }

    private void DisplayRemainingHealth()
    {
        float remainingHealth = playerHealth.CurrentHealth;

        // Check current language and display health accordingly
        if (localizationManager.CurrentLanguage == Language.English)
        {
            healthDisplay.text = $"Health Remaining: {remainingHealth}%";
        }
        else if (localizationManager.CurrentLanguage == Language.Indonesian)
        {
            healthDisplay.text = $"Sisa Nyawa: {remainingHealth}%";
        }
    }
}
