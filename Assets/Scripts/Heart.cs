using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField]
    private float healthAmount;

    private void Awake()
    {
        healthAmount = Random.Range(10, 21);  // Randomize healthAmount value
    }

    void Update()
    {
        // Get the player game object
        GameObject player = GameObject.Find("Player");
        // Check if the player is within 10 units of the item
        if (IsPlayerInRange(player))
        {
            // Move the item towards the player
            MoveItemToPlayer(player);
        }
    }


    // This method is called from the Item script to apply health to the player
    public void ApplyHealthToPlayer(GameObject player)
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.AddHealth(healthAmount);
            playerHealth.UpdateHealthBar();
        }
    }

    public float GetHealthAmount()
    {
        return healthAmount;
    }

     // A method that moves the item towards the player
    void MoveItemToPlayer(GameObject player)
    {
        // Get the transform component of the item
        Transform itemTransform = GetComponent<Transform>();
        // Get the transform component of the player
        Transform playerTransform = player.GetComponent<Transform>();
        // Move the item towards the player by 5 units per frame
        itemTransform.position = Vector3.MoveTowards(itemTransform.position, playerTransform.position, 10 * Time.deltaTime);
    }

    // A method that checks if the player is within 10 units of the item
    bool IsPlayerInRange(GameObject player)
    {
        // Get the transform component of the item
        Transform itemTransform = GetComponent<Transform>();
        // Get the transform component of the player
        Transform playerTransform = player.GetComponent<Transform>();
        // Calculate the distance between the item and the player
        float distance = Vector3.Distance(itemTransform.position, playerTransform.position);
        // Return true if the distance is less than or equal to 10, false otherwise
        return distance <= 2;
    }
}
