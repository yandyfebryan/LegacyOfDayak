using UnityEngine;

public enum GameplayLanguage
{
    English,
    Indonesian
}

[CreateAssetMenu(fileName = "GameplayLocalizationData", menuName = "Localization/GameplayLocalizationData")]
public class GameplayLocalizationData : ScriptableObject
{
    public Language language; // Change to Language enum

    [Header("UI")]
    public string checkpointText;

    [Header("Pause Menu")]
    public string pauseMenuText;
    public string settingsButton;
    public string resumeButton;
    public string mainMenuButton;
    public string restartButton;
    
    [Header("Settings Menu")]
    public string settingsMenuTitle;
    public string musicSlider;
    public string sfxSlider;
    public string languageText;
    public string englishText;
    public string indonesianText;

    [Header("GameOver Menu")]
    public string gameOverMenuText;
    public string reloadText;
    public string mainMenuText;

    [Header("LevelCompleted Menu")]
    public string levelCompletedMenuText;
    public string nextLevelButton;
    public string restartButon;
    public string mainMenuButtonText;
    public string star1Text;
    public string star2Text;
    public string star3Text;

    [Header("Enemies")]
    public string boarText;
    public string slugText;
}
