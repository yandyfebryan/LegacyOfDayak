using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public float damage = 10f; // The amount of damage the weapon can inflict
    public LayerMask enemyLayer; // The layer mask that defines what is an enemy
    public float damageInterval = 0.5f; // The time interval between each damage in seconds

    private BoxCollider2D boxCollider; // The box collider 2D component of the weapon
    private float damageTimer; // The timer to keep track of the damage cooldown

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>(); // Get the box collider 2D component of the weapon
        damageTimer = 0f; // Reset the damage timer to zero
    }

    void Update()
    {
        damageTimer -= Time.deltaTime; // Update the damage timer by subtracting the time since last frame

        if (damageTimer <= 0f)
        {
            Collider2D hit = Physics2D.OverlapBox(boxCollider.bounds.center, boxCollider.bounds.size, 0f, enemyLayer);
            if (hit != null && hit.CompareTag("Enemy"))
            {
                EnemyHealth health = hit.GetComponent<EnemyHealth>();
                if (health != null)
                {
                    Vector2 attackDirection = (hit.transform.position - transform.position).normalized; // Calculate the attack direction
                    health.TakeDamage(damage, attackDirection); // Apply damage with attack direction
                    damageTimer = damageInterval; // Reset the damage timer to the damage interval
                }
            }
        }
    }
}
