using UnityEngine;
using UnityEngine.Audio;

public class Projectile_Sumpit : MonoBehaviour
{
    public float speed = 10f;
    public float lifeDuration = 5f;
    public float damage = 5f; // Damage value for the projectile
    public LayerMask enemyLayer; // Define the enemy layer here
    public LayerMask groundLayer; // Layer for the ground
    //public float groundCheckRadius = 0.2f; // Radius for ground check
    public Transform frontCheck; // Transform to check for ground at the front of the projectile
    //public Transform groundCheck; // Transform to check for ground

    private bool hasHitGround = false;
    private Collider2D projectileCollider; // Reference to the projectile's collider

    [Header("Audio")]
    [SerializeField] private AudioClip groundHitSound; // Ground hit sound effect
    [SerializeField] private AudioMixerGroup outputMixerGroup; // Output audio mixer group

    private bool hasBeenActivated = false; // Flag to indicate the projectile has been activated

    private void Start()
    {
        Destroy(gameObject, lifeDuration);
        projectileCollider = GetComponent<Collider2D>(); // Get the Collider component
    }

    private void Update()
    {
        if (!hasHitGround)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        GroundCheck();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
        {
            EnemyHealth enemyHealth = collision.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                Vector2 attackDirection = (collision.transform.position - transform.position).normalized;
                enemyHealth.TakeDamage(damage, attackDirection);
                Destroy(gameObject);
            }
        }
        else
        {
            CheckIfHitGround(collision);
        }
    }

   private void GroundCheck()
    {
        if (hasHitGround) return;

        // Check if the front point of the projectile is touching the ground
        Collider2D groundCollider = Physics2D.OverlapPoint(frontCheck.position, groundLayer);
        if (groundCollider != null)
        {
            StickToGround();
        }
    }

    private void CheckIfHitGround(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            StickToGround();
        }
    }

    private void StickToGround()
    {
        hasHitGround = true;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
        }
        if (projectileCollider != null)
        {
            projectileCollider.enabled = false; // Disable the collider
        }

        PlayGroundHitSound(); // Play the ground hit sound
    }

    public void ActivateProjectile()
    {
        hasBeenActivated = true;
    }

    private void PlayGroundHitSound()
    {
        if (groundHitSound != null && hasBeenActivated)
        {
            // Create a new temporary audio source at this position
            AudioSource tempAudioSource = gameObject.AddComponent<AudioSource>();
            // Configure AudioSource settings
            tempAudioSource.clip = groundHitSound;
            tempAudioSource.playOnAwake = false;
            tempAudioSource.volume = 0.8f; // Adjust as needed
            tempAudioSource.outputAudioMixerGroup = outputMixerGroup;
            // Play the sound
            tempAudioSource.Play();
            // Destroy the AudioSource after the clip has finished playing
            Destroy(tempAudioSource, groundHitSound.length);
        }
        else
        {
            Debug.LogWarning("groundHitSound not set or projectile not activated");
        }
    }

    // private void OnDrawGizmosSelected()
    // {
    //     if (groundCheck != null)
    //     {
    //         Gizmos.color = Color.yellow;
    //         Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    //     }
    // }
}
