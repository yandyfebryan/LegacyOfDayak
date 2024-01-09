using UnityEngine;

public class WoodItem : MonoBehaviour
{
    [SerializeField] private Sprite icon; // Wood icon
    [SerializeField] private FeedbackEffect feedbackEffect; // Feedback effect for wood collection
    [SerializeField] private int woodAmount = 1; // Amount of wood provided by this item, configurable in Unity Editor

    private ItemCollectionTracker itemCollectionTracker; // Tracker for wood collection objective
    private RangedAttack playerRangedAttack; // Cached component, if needed for additional interactions

    private void Awake()
    {
        MySceneManager.OnCheckpointReload += UpdateFeedbackEffect;
        itemCollectionTracker = FindObjectOfType<ItemCollectionTracker>(); // Find and cache the item collection tracker
    }

    private void OnDestroy()
    {
        MySceneManager.OnCheckpointReload -= UpdateFeedbackEffect;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HandleWoodPickup(collision);
            itemCollectionTracker?.ItemCollected(); // Notify the tracker that a wood item has been collected
            Destroy(gameObject); // Destroy the wood item after pickup
        }
    }

    private void HandleWoodPickup(Collider2D collision)
    {
        EnsureFeedbackEffect("WoodPickupFeedback/Image"); // Ensure feedback effect for wood pickup
        feedbackEffect.ShowWithWoodAmount(woodAmount); // Show feedback effect with the amount of wood collected
    }

    private void EnsureFeedbackEffect(string feedbackPath)
    {
        if (feedbackEffect == null)
        {
            GameObject playerHolder = GameObject.FindGameObjectWithTag("Player");
            Transform imageTransform = playerHolder.transform.Find(feedbackPath);
            feedbackEffect = imageTransform.GetComponent<FeedbackEffect>();
        }
    }

    private void UpdateFeedbackEffect()
    {
        // Update feedback effect for wood collection
        EnsureFeedbackEffect("WoodPickupFeedback/Image");
    }
}
