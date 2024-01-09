using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GallMan : MonoBehaviour
{
    // Public fields to assign in the editor
    public GameObject galleryMenu;
    public GameObject thumbnailGrid; // Grid panel for thumbnails
    public GameObject thumbnailPrefab; // Reference to the thumbnail prefab

    public List<Section> sections; // List of sections
    public TextMeshProUGUI sectionNameText;

    [Header("Detailed View Element")]
    public GameObject detailedView; // Panel for detailed view
    public Image detailedImage1;
    public Image detailedImage2;
    public TextMeshProUGUI detailedDescriptionTextEN; // English description text
    public TextMeshProUGUI detailedDescriptionTextID; // Indonesian description text
    public TextMeshProUGUI detailedCaptionText; // Caption text in the detailed view
    
    [Header("Buttons")]
    public Button nextButton; //Button for next section navigation
    public Button prevButton; // Button for previous section navigation
    public GameObject thumbnailButtonPrefab; // Reference to the button prefab

    public List<Image> circleIndicators;
    public Color activeIndicatorColor = Color.green; // Default active color
    public Color inactiveIndicatorColor = Color.gray; // Default inactive color
    private int currentSectionIndex = 0;

    // Unity Start method
    void Start()
    {
        ShowSection(currentSectionIndex);
        UpdateCircleIndicators();
    }

    // Public methods for UI interaction and display
    public void ShowSection(int index)
    {
        ClearThumbnailGrid();

        foreach (var thumbnail in sections[index].thumbnails)
        {
            CreateThumbnail(thumbnail);
        }

        UpdateSectionName(index);
    }

    public void ShowDetailedView(Thumbnail thumbnail)
    {
        detailedImage1.sprite = thumbnail.image1;
        detailedImage2.sprite = thumbnail.image2;
        
        // Check the language setting and show the appropriate description text
        int languageSetting = PlayerPrefs.GetInt("LanguageSetting", 0); // Default to English
        if (languageSetting == 0)
        {
            detailedDescriptionTextEN.text = thumbnail.descriptionEN;
            detailedDescriptionTextEN.gameObject.SetActive(true);
            detailedDescriptionTextID.gameObject.SetActive(false);
        }
        else if (languageSetting == 1)
        {
            detailedDescriptionTextID.text = thumbnail.descriptionID;
            detailedDescriptionTextID.gameObject.SetActive(true);
            detailedDescriptionTextEN.gameObject.SetActive(false);
        }

        detailedCaptionText.text = thumbnail.caption;

        detailedView.SetActive(true); // Show the detailed view panel
        galleryMenu.SetActive(false);
    }

    public void CloseDetailedView()
    {
        detailedView.SetActive(false);
        galleryMenu.SetActive(true);
    }

    public void NextSection()
    {
        if (currentSectionIndex < sections.Count - 1)
        {
            currentSectionIndex++;
            ShowSection(currentSectionIndex);
            UpdateCircleIndicators();
        }
    }

    public void PreviousSection()
    {
        if (currentSectionIndex > 0)
        {
            currentSectionIndex--;
            ShowSection(currentSectionIndex);
            UpdateCircleIndicators();
        }
    }

    // Private helper methods for internal functionality
    private void UpdateCircleIndicators()
    {
        for (int i = 0; i < circleIndicators.Count; i++)
        {
            circleIndicators[i].color = (i == currentSectionIndex) ? activeIndicatorColor : inactiveIndicatorColor;
        }
    }

    private void ClearThumbnailGrid()
    {
        foreach (Transform child in thumbnailGrid.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void CreateThumbnail(Thumbnail thumbnail)
    {
        GameObject thumbnailObj = Instantiate(thumbnailPrefab, thumbnailGrid.transform);
        GameObject buttonObj = Instantiate(thumbnailButtonPrefab, thumbnailObj.transform);

        Image thumbImage = thumbnailObj.transform.GetChild(2).GetComponent<Image>();
        thumbImage.sprite = thumbnail.image1;

        TextMeshProUGUI captionText = thumbnailObj.GetComponentInChildren<TextMeshProUGUI>();
        captionText.text = thumbnail.caption;

        Button thumbButton = buttonObj.GetComponent<Button>();
        thumbButton?.onClick.AddListener(() => ShowDetailedView(thumbnail));
    }

    private void UpdateSectionName(int index)
    {
        int languageSetting = PlayerPrefs.GetInt("LanguageSetting", 0); // Default to English

        if (languageSetting == 0)
        {
            sectionNameText.text = sections[index].sectionNameEN;
        }
        else if (languageSetting == 1)
        {
            sectionNameText.text = sections[index].sectionNameID;
        }
    }

    public void ActivateGalleryMenu()
    {
        galleryMenu.SetActive(true);
        UpdateSectionName(currentSectionIndex);
    }
}
