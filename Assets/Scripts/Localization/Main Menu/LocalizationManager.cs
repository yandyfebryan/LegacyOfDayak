using System.IO;
using UnityEngine;
using TMPro;

public class LocalizationManager : MonoBehaviour
{
    public LocalizationData[] localizations; // Array to hold different language data

    [Header("Main Menu")]
    public TextMeshProUGUI startPlaying;
    public TextMeshProUGUI settingsButton;
    public TextMeshProUGUI galleryButton;
    public TextMeshProUGUI informationButton;
    public TextMeshProUGUI exitButton;

    [Header("Select Level Menu")]
    public TextMeshProUGUI selectLevelText;
    public TextMeshProUGUI loadingPanelText;

    [Header("Select Level Menu - Level 1")]
    public TextMeshProUGUI level1Start;
    public TextMeshProUGUI level1ObjectiveText;
    public TextMeshProUGUI level1ObjectiveDetailText;
    public TextMeshProUGUI level1StoryText;
    public TextMeshProUGUI level1StoryDetailText;

    [Header("Select Level Menu - Level 2")]
    public TextMeshProUGUI level2Start;
    public TextMeshProUGUI level2ObjectiveText;
    public TextMeshProUGUI level2ObjectiveDetailText;
    public TextMeshProUGUI level2StoryText;
    public TextMeshProUGUI level2StoryDetailText;

    [Header("Select Level Menu - Level 3")]
    public TextMeshProUGUI level3Start;
    public TextMeshProUGUI level3ObjectiveText;
    public TextMeshProUGUI level3ObjectiveDetailText;
    public TextMeshProUGUI level3StoryText;
    public TextMeshProUGUI level3StoryDetailText;

    [Header("Main Menu - Confirm Exit")]
    public TextMeshProUGUI confirmExitText;
    public TextMeshProUGUI yesText;
    public TextMeshProUGUI noText;

    [Header("Settings Menu")]
    public TextMeshProUGUI settingsMenuTitle;
    public TextMeshProUGUI musicSlider;
    public TextMeshProUGUI sfxSlider;
    public TextMeshProUGUI languageText;
    public TextMeshProUGUI englishText;
    public TextMeshProUGUI indonesianText;

    [Header("Information Menu")]
    public TextMeshProUGUI informationMenuTitle;
    public TextMeshProUGUI aboutButton;
    public TextMeshProUGUI profileButton;

    [Header("Information Submenu - Profile")]
    public TextMeshProUGUI profileMenuTitle;
    public TextMeshProUGUI studentName;
    public TextMeshProUGUI studentID;
    public TextMeshProUGUI studentMajor;
    public TextMeshProUGUI studentFaculty;
    public TextMeshProUGUI studentUniversity;

    [Header("Information Submenu - About")]
    public TextMeshProUGUI aboutMenuTitle;
    public TextMeshProUGUI aboutText;

    [Header("Gallery Menu")]
    public TextMeshProUGUI galleryMenuTitle;

    private Language currentLanguage;

    public Language CurrentLanguage => currentLanguage;

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

        // Store the language setting
        PlayerPrefs.SetInt("LanguageSetting", (int)language);

        foreach (var data in localizations)
        {
            if (data.language == language)
            {
                UpdateLanguage(data);
                break;
            }
        }
    }

    private void UpdateLanguage(LocalizationData data)
    {
        //Main Menu
        startPlaying.text = data.startPlaying;
        settingsButton.text = data.settingsButton;
        galleryButton.text = data.galleryButton;
        informationButton.text = data.informationButton;
        exitButton.text = data.exitButton;

        //Main Menu - Confirm Exit
        confirmExitText.text = data.confirmExitText;
        yesText.text = data.yesText;
        noText.text = data.noText;

        //Select Level Menu
        selectLevelText.text = data.selectLevelText;
        loadingPanelText.text = data.loadingPanelText;

        //Select Level Menu - Level 1
        level1Start.text = data.level1Start;
        level1ObjectiveText.text = data.level1ObjectiveText;
        level1ObjectiveDetailText.text = data.level1ObjectiveDetailText;
        level1StoryText.text = data.level1StoryText;
        level1StoryDetailText.text = data.level1StoryDetailText;

        // Select Level Menu - Level 2
        level2Start.text = data.level2Start;
        level2ObjectiveText.text = data.level2ObjectiveText;
        level2ObjectiveDetailText.text = data.level2ObjectiveDetailText;
        level2StoryText.text = data.level2StoryText;
        level2StoryDetailText.text = data.level2StoryDetailText;

        // Select Level Menu - Level 3
        level3Start.text = data.level3Start;
        level3ObjectiveText.text = data.level3ObjectiveText;
        level3ObjectiveDetailText.text = data.level3ObjectiveDetailText;
        level3StoryText.text = data.level3StoryText;
        level3StoryDetailText.text = data.level3StoryDetailText;
        
        //Settings Menu
        settingsMenuTitle.text = data.settingsMenuTitle;
        musicSlider.text = data.musicSlider;
        sfxSlider.text = data.sfxSlider;
        languageText.text = data.languageText;
        englishText.text = data.englishText;
        indonesianText.text = data.indonesianText;

        //Information Menu
        informationMenuTitle.text = data.informationMenuTitle;
        aboutButton.text = data.aboutButton;
        profileButton.text = data.profileButton;

        //Information Submenu - Profile
        profileMenuTitle.text = data.profileMenuTitle;
        studentName.text = data.studentName;
        studentID.text = data.studentID;
        studentMajor.text = data.studentMajor;
        studentFaculty.text = data.studentFaculty;
        studentUniversity.text = data.studentUniversity;

        //Information Submenu - About
        aboutMenuTitle.text = data.aboutMenuTitle;
        aboutText.text = data.aboutText;

        //Gallery Menu
        galleryMenuTitle.text = data.galleryMenuTitle;
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
