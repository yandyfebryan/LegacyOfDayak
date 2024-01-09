using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Sprite icon;
    [SerializeField] private int ammoAmount = 1; // Configurable through the Unity Editor

    private Heart heartComponent;
    private Ammo ammoComponent;
    private RangedAttack playerRangedAttack; // Cached component

    private void Awake()
    {
        heartComponent = GetComponent<Heart>();
        ammoComponent = GetComponent<Ammo>();
        // Removed the subscription to UpdateFeedbackEffect
    }

    private void OnDestroy()
    {
        // Removed the unsubscription to UpdateFeedbackEffect
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CachePlayerComponents(collision);

            if (heartComponent != null)
            {
                HandleHeartPickup(collision);
            }
            else if (ammoComponent != null)
            {
                HandleAmmoPickup(collision);
            }

            Destroy(gameObject);
        }
    }

    private void CachePlayerComponents(Collider2D collision)
    {
        if (playerRangedAttack == null)
        {
            playerRangedAttack = collision.gameObject.GetComponent<RangedAttack>();
        }
    }

    private void HandleHeartPickup(Collider2D collision)
    {
        float healthAmount = heartComponent.GetHealthAmount();
        if (PlayerManager.Instance.HeartFeedbackEffect != null)
        {
            PlayerManager.Instance.HeartFeedbackEffect.ShowWithHealthAmount((int)healthAmount);
        }
        heartComponent.ApplyHealthToPlayer(collision.gameObject);
    }

    private void HandleAmmoPickup(Collider2D collision)
    {
        if (PlayerManager.Instance.AmmoFeedbackEffect != null)
        {
            PlayerManager.Instance.AmmoFeedbackEffect.ShowWithAmmoAmount(ammoAmount);
        }

        if (playerRangedAttack != null)
        {
            playerRangedAttack.AddAmmo(ammoAmount);
        }
    }
}
