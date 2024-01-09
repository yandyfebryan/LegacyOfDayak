using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    [Header("Damage Settings")]
    [SerializeField] private float damageAmount = 20f;

    [Header("Knockback Settings")]
    [SerializeField] private float knockbackStrength = 4.0f;
    [SerializeField] private float verticalKnockbackStrength = 2.0f;
    [SerializeField] private Transform enemyFront; // Enemy's front transform

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Checking for Player Tag
        if (collision.collider.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();
            Rigidbody2D playerRb = collision.collider.GetComponent<Rigidbody2D>();
            
            if (playerHealth != null) // In case the PlayerHealth component isn't attached for some reason
            {
                playerHealth.TakeDamage(damageAmount);
            }

            if (playerRb != null && enemyFront != null) // Check if both Rigidbody2D and enemy Front transform are attached
            {
                ApplyKnockback(collision, playerRb);
            }
        }
    }

    private void ApplyKnockback(Collision2D collision, Rigidbody2D playerRb)
    {
        // Determine the direction of the collision relative to the enemy's front
        Vector2 directionToCollision = collision.transform.position - enemyFront.position;
        bool isCollisionInFrontOfEnemy = directionToCollision.x < 0;

        // Set the knockback direction relative to enemy's front
        Vector2 knockbackDirection = isCollisionInFrontOfEnemy ? Vector2.left : Vector2.right;
        knockbackDirection += Vector2.up * verticalKnockbackStrength; // Adding vertical force
        knockbackDirection.Normalize();

        // Apply the knockback force with a multiplier for strength
        playerRb.velocity = knockbackDirection * knockbackStrength;
    }
}
