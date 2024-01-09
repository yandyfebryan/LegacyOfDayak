using TMPro;
using UnityEngine;

public class EnemyLocalization : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private EnemyType enemyType; // Add an enum for enemy types

    private void Start()
    {
        if (textComponent == null)
        {
            Debug.LogError("TextMeshProUGUI component not assigned in EnemyLocalization.");
            return;
        }

        UpdateLocalization();
    }

    private void UpdateLocalization()
    {
        var manager = FindObjectOfType<GameplayLocalizationManager>();
        if (manager != null)
        {
            var localizationData = manager.GetCurrentLocalization();
            if (localizationData != null)
            {
                switch (enemyType) // Choose text based on enemy type
                {
                    case EnemyType.Boar:
                        textComponent.text = localizationData.boarText;
                        break;
                    case EnemyType.Slug:
                        textComponent.text = localizationData.slugText;
                        break;
                    // Add cases for other enemy types here
                }
            }
        }
    }

    private void OnEnable()
    {
        GameplayLocalizationManager.OnLanguageChanged += UpdateLocalization;
    }

    private void OnDisable()
    {
        GameplayLocalizationManager.OnLanguageChanged -= UpdateLocalization;
    }
}

public enum EnemyType
{
    Boar,
    Slug
    // Add other enemy types here
}
