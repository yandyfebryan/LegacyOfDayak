using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    // The amount of damage the enemy can inflict
    public float damage = 15f;

    // The layer mask that defines what is a player
    public LayerMask playerLayer;

    // The box collider 2D component of the enemy
    private BoxCollider2D boxCollider;

    // The time interval between each damage in seconds
    public float damageInterval = 0.5f;

    // The timer to keep track of the damage cooldown
    private float damageTimer;

    // Start is called before the first frame update
    void Start()
    {
        // Get the box collider 2D component of the enemy
        boxCollider = GetComponent<BoxCollider2D>();

        // Reset the damage timer to zero
        damageTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Update the damage timer by subtracting the time since last frame
        damageTimer -= Time.deltaTime;

        // Check if the enemy attack is colliding with player and the damage timer is less than or equal to zero
        if (damageTimer <= 0f && Physics2D.OverlapBox(boxCollider.bounds.center, boxCollider.bounds.size, 0f, playerLayer))
        {
            // Get the first collider that is hit by the weapon
            Collider2D hit = Physics2D.OverlapBox(boxCollider.bounds.center, boxCollider.bounds.size, 0f, playerLayer);

            // Check if the collider has the "Player" tag
            if (hit.CompareTag("Player"))
            {
                // Get the health script component of the collider
                PlayerHealth health = hit.GetComponent<PlayerHealth>();

                // Apply damage to the health
                health.TakeDamage(damage);

                // Reset the damage timer to the damage interval
                damageTimer = damageInterval;
            }
        }
    }
}
