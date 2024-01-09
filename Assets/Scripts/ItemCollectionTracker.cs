using UnityEngine;
using TMPro; // Include TextMeshPro namespace

public class ItemCollectionTracker : MonoBehaviour
{
    public GameObject[] collectibleItems;

    // Reference for displaying the number of items left to collect
    public TextMeshProUGUI itemsLeftText;

    public delegate void ObjectiveCompletedAction();
    public event ObjectiveCompletedAction OnObjectiveCompleted;

    private int totalItems;
    private int collectedItems;

    void Start()
    {
        totalItems = collectibleItems.Length;
        collectedItems = 0;
        UpdateItemCollectionDisplay();
    }

    // Call this method when an item is collected
    public void ItemCollected()
    {
        collectedItems++;
        UpdateItemCollectionDisplay();

        if (collectedItems >= totalItems)
        {
            ObjectiveCompleted();
        }
    }

    private void UpdateItemCollectionDisplay()
    {
        if (itemsLeftText != null)
        {
            int itemsLeft = totalItems - collectedItems;
            itemsLeftText.text = $"Items Left: {itemsLeft}";
        }
    }

    private void ObjectiveCompleted()
    {
        if (OnObjectiveCompleted != null)
        {
            OnObjectiveCompleted();
        }
    }
}
