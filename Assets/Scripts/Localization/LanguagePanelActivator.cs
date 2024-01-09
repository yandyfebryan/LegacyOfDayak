using UnityEngine;

public class LanguagePanelActivator : MonoBehaviour
{
    public GameObject englishGameObject; // Assign in the inspector
    public GameObject indonesianGameObject; // Assign in the inspector

    private void Start()
    {
        UpdateLanguageVisualization();
    }

    private void UpdateLanguageVisualization()
    {
        // Retrieve the current language setting from PlayerPrefs
        Language currentLanguage = (Language)PlayerPrefs.GetInt("LanguageSetting", (int)Language.English); // Default to English if not set

        // Activate the corresponding GameObject and deactivate the other one
        englishGameObject.SetActive(currentLanguage == Language.English);
        indonesianGameObject.SetActive(currentLanguage == Language.Indonesian);
    }
}

public enum PanelLanguage
{
    English,
    Indonesian
}
