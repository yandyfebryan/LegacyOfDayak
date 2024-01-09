using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject playerHolder; // Reference to the PlayerHolder object
    [SerializeField] private MySceneManager mySceneManager; // Reference to the MySceneManager

    [Header("Health Parameters")]
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    public float MaxHealth
    {
        get { return maxHealth; }
    }

    public float CurrentHealth
    {
        get { return currentHealth; }
    }

    public void AddHealth(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthBar(); // Update the health UI here after health change
        CheckDeathCondition(); // New position for death check
    }

    [Header("UI Elements")]
    [SerializeField] private Image emptyHealthBar;
    [SerializeField] private Image filledHealthBar;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Canvas playerHealthBar;
    [SerializeField] private Canvas gameOverCanvas;
    [SerializeField] private Canvas onScreenButton;
    [SerializeField] private Canvas healthBar;
    [SerializeField] private StarScorer starScorer;
    [SerializeField] private GameObject timer;
    [SerializeField] private GameObject sumpit;

    #region Animations and Visuals
    private Animator animator;
    private bool isInvincible = false;
    [SerializeField] private float invincibilityDuration = 1f;
    private Color originalColor;
    [SerializeField] private float flashInterval = 0.1f;
    private Color flashColor = Color.red;
    private SpriteRenderer spriteRenderer;
    private bool isCoroutineRunning = false;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource; // Reference to the audio source component
    [SerializeField] private AudioClip hurtSound; // Hurt sound effect

    #endregion

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void Update()
    {
        CheckDeathCondition();
    }

    private void CheckDeathCondition()
    {
        if (currentHealth <= 0f)
        {
            animator.Play("Death");
        }
    }

    public void Die()
    {
        //this.enabled = false; // Consider if you need this
        DisableUI();
        gameOverCanvas.gameObject.SetActive(true);
        TriggerRespawnProcess(); // Ensure this handles respawn logic appropriately
    }

    private void TriggerRespawnProcess()
    {
        gameObject.SetActive(false); // Disable the Player GameObject, which is where the PlayerHealth script attached to
    }

    // Method to reset health to full
    public void ResetHealthToFull()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();

        // Reset invincibility
        isInvincible = false;
        StopCoroutine(BecomeTemporarilyInvincible());
        spriteRenderer.color = originalColor;

        // Reset animation state if needed
        animator.SetTrigger("ResetState");
    }

     public void TakeDamage(float damage)
    {
        if (!isInvincible)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
            UpdateHealthBar();
            CheckDeathCondition();

            PlayHurtSound(); // Play hurt sound

            if (!isCoroutineRunning)
            {
                StartCoroutine(BecomeTemporarilyInvincible());
            }
        }
    }

    // Method to play the hurt sound
    private void PlayHurtSound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = hurtSound;
            audioSource.Play();
        }
    }

    // A method to update the health bar UI
    public void UpdateHealthBar()
    {
        // Calculate the health ratio between zero and one
        float healthRatio = currentHealth / maxHealth;

        // Set the fill amount of the filled health bar image to the health ratio
        filledHealthBar.fillAmount = healthRatio;

        // Set the text of the health text element to the current and maximum health in percentage format
        healthText.text = $"{healthRatio * 100}%";
    }

    public void DisableUI()
    {
        onScreenButton.gameObject.SetActive(false);
        healthBar.gameObject.SetActive(false);
        timer.gameObject.SetActive(false);
        sumpit.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Check if the other gameobject has a tag named "PitFall"
        if (collider.CompareTag("PitFall"))
        {
            //this.enabled = false; // Consider if you need this
            DisableUI();
            gameOverCanvas.gameObject.SetActive(true);
            TriggerRespawnProcess(); // Ensure this handles respawn logic appropriately
        }
    }

    // A coroutine to make the player temporarily invincible and flash
    private IEnumerator BecomeTemporarilyInvincible()
    {
        isCoroutineRunning = true; // Set the flag when the coroutine starts
        isInvincible = true;

        float elapsedTime = 0; // Track the passed time 

        while (elapsedTime < invincibilityDuration)
        {
            spriteRenderer.color = spriteRenderer.color == originalColor ? flashColor : originalColor;
            elapsedTime += flashInterval;
            yield return new WaitForSeconds(flashInterval);
        }

        spriteRenderer.color = originalColor; // Reset the color
        isInvincible = false; // End of invincibility

        isCoroutineRunning = false;
    }
}
