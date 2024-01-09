using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public FeedbackEffect HeartFeedbackEffect;
    public FeedbackEffect AmmoFeedbackEffect;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Initialize the FeedbackEffect references
            HeartFeedbackEffect = transform.Find("HeartPickupFeedback/Image").GetComponent<FeedbackEffect>();
            AmmoFeedbackEffect = transform.Find("AmmoPickupFeedback/Image").GetComponent<FeedbackEffect>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
