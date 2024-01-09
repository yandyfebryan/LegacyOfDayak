using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GalleryManager : MonoBehaviour
{
    [SerializeField] private List<Gallery> gallery;
    public Image mainImageView;
    public Image nextImageView;
    public Image previousImageView;
    public TMP_Text descriptionTextBox;
    public Button nextButton;
    public Button previousButton;

    private int currentIndex = 0;

    private void Start()
    {
        if (gallery == null || gallery.Count == 0)
        {
            Debug.LogError("Gallery list is empty or not set!");
            return;
        }

        if (mainImageView == null || nextImageView == null || previousImageView == null)
        {
            Debug.LogError("One or more UI components are not assigned!");
            return;
        }

        ShowGallery(currentIndex);
    }

    public void ShowGallery(int index)
    {
        if (index >= 0 && index < gallery.Count)
        {
            currentIndex = index;
            mainImageView.sprite = gallery[index].galleryImage;
            descriptionTextBox.text = gallery[index].galleryDescription;

            SetNextImage(index);
            SetPreviousImage(index);

            UpdateButtonInteractivity();
        }
    }

    private void SetNextImage(int index)
    {
        nextImageView.sprite = (index < gallery.Count - 1) ? gallery[index + 1].galleryImage : null;
    }

    private void SetPreviousImage(int index)
    {
        previousImageView.sprite = (index > 0) ? gallery[index - 1].galleryImage : null;
    }

    private void UpdateButtonInteractivity()
    {
        nextButton.interactable = currentIndex < gallery.Count - 1;
        previousButton.interactable = currentIndex > 0;
    }

    public void NextGallery()
    {
        if (currentIndex < gallery.Count - 1)
        {
            currentIndex++;
            ShowGallery(currentIndex);
        }
    }

    public void PrevGallery()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            ShowGallery(currentIndex);
        }
    }
}
