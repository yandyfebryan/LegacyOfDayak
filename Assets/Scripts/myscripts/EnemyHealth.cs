using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class EnemyHealth : MonoBehaviour
{
    private Rigidbody2D rb;

    public float maxHealth = 100f;
    public float currentHealth;
    public Image emptyHealthBar;
    public Image filledHealthBar;
    public CanvasGroup enemyHealthBarGroup;
    public float hideTime = 5f;
    public Vector3 healthBarOffset = new Vector3(0, 2.0f, 0);
    public float damageCooldown = 2.0f;

    private Animator animator;
    private bool isDead = false;
    private float hideHealthBarTimer = 0;

    private bool recentlyDamaged = false;
    private float timeSinceLastDamage = 0;

    private Vector2 lastDamageDirection;

    public bool RecentlyDamaged
    {
        get { return recentlyDamaged; }
    }

    private Vector3 previousPosition;

    [Header("Audio")]
    [SerializeField] private AudioClip enemyHurtSound; // Enemy hurt sound effect
    [SerializeField] private AudioClip enemyDeathSound; // Enemy death sound effect
    [SerializeField] private AudioMixerGroup outputMixerGroup; // Output audio mixer group
    [SerializeField] private float hurtSoundVolume = 0.8f; // Adjustable volume

    private bool hasBeenActivated = false; // Flag to check if the enemy has been activated

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        hasBeenActivated = true; // Set the enemy as activated

        currentHealth = maxHealth;
        UpdateHealthBar();

        animator = GetComponent<Animator>();

        enemyHealthBarGroup.alpha = 0f;
        enemyHealthBarGroup.interactable = false;
        enemyHealthBarGroup.blocksRaycasts = false;

        previousPosition = transform.position;
    }

    void Update()
    {
        if (HasMoved())
        {
            enemyHealthBarGroup.transform.position = transform.position + healthBarOffset;
        }

        if (recentlyDamaged)
        {
            timeSinceLastDamage += Time.deltaTime;

            if (timeSinceLastDamage >= damageCooldown)
            {
                recentlyDamaged = false;
            }
        }

        if (hideHealthBarTimer > 0)
        {
            hideHealthBarTimer -= Time.deltaTime;
            if (hideHealthBarTimer <= 0)
            {
                HideHealthBar();
            }
        }
    }

    private bool HasMoved()
    {   
        bool hasMoved = Vector3.SqrMagnitude(transform.position - previousPosition) > 0.00001f;

        previousPosition = transform.position;

        return hasMoved;
    }

    public void TakeDamage(float damage, Vector2 attackDirection)
    {
        lastDamageDirection = attackDirection;
        if (isDead)
            return;

        EnemyKnockback knockback = GetComponent<EnemyKnockback>();
        if (knockback != null)
        {
            knockback.ApplyKnockback(attackDirection);
        }

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthBar();

        enemyHealthBarGroup.alpha = 1f;
        enemyHealthBarGroup.interactable = true;
        enemyHealthBarGroup.blocksRaycasts = true;
        hideHealthBarTimer = hideTime;

        recentlyDamaged = true;
        timeSinceLastDamage = 0;

        if (currentHealth <= 0f)
        {
            Die();
        }
        else
        {
            PlayHurtSound(); // Play the hurt sound only if the enemy is not dead
        }
    }

    private void PlayHurtSound()
    {
        if (enemyHurtSound != null && hasBeenActivated)
        {
            AudioSource tempAudioSource = gameObject.AddComponent<AudioSource>();
            tempAudioSource.clip = enemyHurtSound;
            tempAudioSource.playOnAwake = false;
            tempAudioSource.volume = hurtSoundVolume;
            tempAudioSource.outputAudioMixerGroup = outputMixerGroup;
            tempAudioSource.Play();

            Destroy(tempAudioSource, enemyHurtSound.length);
        }
        else
        {
            Debug.LogWarning("enemyHurtSound not set or enemy not activated");
        }
    }

    public Vector2 GetLastDamageDirection()
    {
        return lastDamageDirection;
    }

    private void UpdateHealthBar()
    {
        float healthRatio = currentHealth / maxHealth;
        filledHealthBar.fillAmount = healthRatio;
    }

    private void HideHealthBar()
    {
        enemyHealthBarGroup.alpha = 0f;
        enemyHealthBarGroup.interactable = false;
        enemyHealthBarGroup.blocksRaycasts = false;
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }
        animator.Play("Death");
        enemyHealthBarGroup.gameObject.SetActive(false);

        PlayDeathSound(); // Play the death sound
    }

    private void PlayDeathSound()
    {
        if (enemyDeathSound != null)
        {
            AudioSource tempAudioSource = gameObject.AddComponent<AudioSource>();
            tempAudioSource.clip = enemyDeathSound;
            tempAudioSource.playOnAwake = false;
            tempAudioSource.volume = hurtSoundVolume; // You can use the same volume control or add a separate one
            tempAudioSource.outputAudioMixerGroup = outputMixerGroup;
            tempAudioSource.Play();

            Destroy(tempAudioSource, enemyDeathSound.length);
        }
        else
        {
            Debug.LogWarning("enemyDeathSound not set");
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (isDead)
            return;

        if (collider.CompareTag("PitFall"))
        {
            Die();
        }
    }
}
