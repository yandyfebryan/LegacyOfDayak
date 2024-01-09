using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField]
    private int ammoAmount;

    private void Awake()
    {
        // Randomize ammo amount value if needed, or set a fixed value
        ammoAmount = Random.Range(1, 2); // Example: randomize between 1 and 2
    }

    // A method that runs every frame
    void Update()
    {
        // Rotate the item around the Y axis
        RotateItem();
        // Get the player game object
        GameObject player = GameObject.Find("Player");
        // Check if the player is within 10 units of the item
        if (IsPlayerInRange(player))
        {
            // Move the item towards the player
            MoveItemToPlayer(player);
        }
    }


    // This method is called from the Item script to add ammo to the player
    public void ApplyAmmoToPlayer(GameObject player)
    {
        RangedAttack rangedAttack = player.GetComponent<RangedAttack>();
        if (rangedAttack != null)
        {
            rangedAttack.AddAmmo(ammoAmount);
        }
    }

    public int GetAmmoAmount()
    {
        return ammoAmount;
    }

    // A method that rotates the item around the Y axis
    void RotateItem()
    {
        // Get the transform component of the item
        Transform itemTransform = GetComponent<Transform>();
        // Rotate the item by 90 degrees per second around the Y axis
        itemTransform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime);
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
