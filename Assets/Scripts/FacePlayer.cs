using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Get the SpriteRenderer component from this GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Check if the player is to the left or right of this NPC/enemy
        if (player != null)
        {
            if (player.position.x < transform.position.x)
            {
                // Player is to the left, face left
                spriteRenderer.flipX = false;
            }
            else if (player.position.x > transform.position.x)
            {
                // Player is to the right, face right
                spriteRenderer.flipX = true;
            }
        }
    }
}
