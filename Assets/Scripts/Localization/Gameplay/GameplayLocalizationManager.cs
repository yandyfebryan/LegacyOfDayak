using System;
using System.IO;
using UnityEngine;
using TMPro;

public class GameplayLocalizationManager : MonoBehaviour
{
    public GameplayLocalizationData[] localizations; // Array for gameplay-specific language data

    [Header("UI")]
    public TextMeshProUGUI checkpointText;

    [Header("Pause Menu")]
    public TextMeshProUGUI pauseMenuText;
    public TextMeshProUGUI settingsButton;
    public TextMeshProUGUI resumeButton;
    public TextMeshProUGUI mainMenuButton;
    public TextMeshProUGUI restartButton;

    [Header("Settings Menu")]
    public TextMeshProUGUI settingsMenuTitle;
    public TextMeshProUGUI musicSlider;
    public TextMeshProUGUI sfxSlider;
    public TextMeshProUGUI languageText;
    public TextMeshProUGUI englishText;
    public TextMeshProUGUI indonesianText;

    [Header("GameOver Menu")]
    public TextMeshProUGUI gameOverMenuText;
    public TextMeshProUGUI reloadText;
    public TextMeshProUGUI mainMenuText;

    [Header("LevelCompleted Menu")]
    public TextMeshProUGUI levelCompletedMenuText;
    public TextMeshProUGUI nextLevelButton;
    public TextMeshProUGUI restartButon;
    public TextMeshProUGUI mainMenuButtonText;
    public TextMeshProUGUI star1Text;
    public TextMeshProUGUI star2Text;
    public TextMeshProUGUI star3Text;

    [Header("Enemies")]
    public TextMeshProUGUI boarText;
    public TextMeshProUGUI slugText;

    private Language currentLanguage;

    public Language CurrentLanguage => currentLanguage;
    public static event Action OnLanguageChanged;

    private void Awake()
    {
        LoadLanguageSetting();
    }

    private void LoadLanguageSetting()
    {
        if (PlayerPrefs.HasKey("LanguageSetting"))
        {
            Language savedLanguage = (Language)PlayerPrefs.GetInt("LanguageSetting");
            SetLanguage(savedLanguage);
        }
    }

    public void SetLanguage(Language language)
    {
        currentLanguage = language;
        PlayerPrefs.SetInt("LanguageSetting", (int)language);

        foreach (var data in localizations)
        {
            if (data.language == language) // This should now work
            {
                UpdateLanguage(data);
                break;
            }
        }

        OnLanguageChanged?.Invoke(); // Notify all listeners
    }

    public GameplayLocalizationData GetCurrentLocalization()
    {
        foreach (var data in localizations)
        {
            if (data.language == currentLanguage)
            {
                return data;
            }
        }
        Debug.LogError("No matching localization data found for current language: " + currentLanguage);
        return null;
    }


    private void UpdateLanguage(GameplayLocalizationData data)
    {
        //UI
        checkpointText.text = data.checkpointText;

        //Pause Menu
        pauseMenuText.text = data.pauseMenuText;
        settingsButton.text = data.settingsButton;
        resumeButton.text = data.resumeButton;
        mainMenuButton.text = data.mainMenuButton;
        restartButton.text = data.restartButton;
               
        //Settings Menu
        settingsMenuTitle.text = data.settingsMenuTitle;
        musicSlider.text = data.musicSlider;
        sfxSlider.text = data.sfxSlider;
        languageText.text = data.languageText;
        englishText.text = data.englishText;
        indonesianText.text = data.indonesianText;   

        //Game Over Menu
        gameOverMenuText.text = data.gameOverMenuText;
        reloadText.text = data.reloadText;
        mainMenuText.text = data.mainMenuText;  

        //Level Completed Menu
        levelCompletedMenuText.text = data.levelCompletedMenuText;
        nextLevelButton.text = data.nextLevelButton;
        restartButon.text = data.restartButon;
        mainMenuButtonText.text = data.mainMenuButtonText;
        star1Text.text = data.star1Text;
        star2Text.text = data.star2Text;
        star3Text.text = data.star3Text;

        //Enemies
        boarText.text = data.boarText;
        slugText.text = data.slugText;
    }

    public void SetLanguageToEnglish()
    {
        SetLanguage(Language.English);
        PlayerPrefs.SetInt("LanguageSetting", (int)Language.English);
    }

    public void SetLanguageToIndonesian()
    {
        SetLanguage(Language.Indonesian);
        PlayerPrefs.SetInt("LanguageSetting", (int)Language.Indonesian);
    }
}
