using UnityEngine;

public enum Language
{
    English,
    Indonesian
}

[CreateAssetMenu(fileName = "LocalizationData", menuName = "Localization/LocalizationData")]
public class LocalizationData : ScriptableObject
{
    public Language language;

    [Header("Main Menu")]
    public string startPlaying;
    public string settingsButton;
    public string galleryButton;
    public string informationButton;
    public string exitButton;

    [Header("Select Level Menu")]
    public string selectLevelText;
    public string loadingPanelText;

    [Header("Select Level Menu - Level 1")]
    public string level1Start;
    public string level1ObjectiveText;
    public string level1ObjectiveDetailText;
    public string level1StoryText;
    public string level1StoryDetailText;

    [Header("Select Level Menu - Level 2")]
    public string level2Start;
    public string level2ObjectiveText;
    public string level2ObjectiveDetailText;
    public string level2StoryText;
    public string level2StoryDetailText;

    [Header("Select Level Menu - Level 3")]
    public string level3Start;
    public string level3ObjectiveText;
    public string level3ObjectiveDetailText;
    public string level3StoryText;
    public string level3StoryDetailText;
    
    [Header("Settings Menu")]
    public string settingsMenuTitle;
    public string musicSlider;
    public string sfxSlider;
    public string languageText;
    public string englishText;
    public string indonesianText;

    [Header("Main Menu - Confirm Exit")]
    public string confirmExitText;
    public string yesText;
    public string noText;

    [Header("Information Menu")]
    public string informationMenuTitle;
    public string aboutButton;
    public string profileButton;

    [Header("Information Submenu - Profile")]
    public string profileMenuTitle;
    public string studentName;
    public string studentID;
    public string studentMajor;
    public string studentFaculty;
    public string studentUniversity;

     [Header("Information Submenu - About")]
    public string aboutMenuTitle;
    public string aboutText;

    [Header("Gallery Menu")]
    public string galleryMenuTitle;
}
