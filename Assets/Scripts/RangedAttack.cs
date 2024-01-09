using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class RangedAttack : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject projectilePrefab;
    public int maxAmmo = 10;
    private int currentAmmo;
    public TextMeshProUGUI ammoText; // Reference to the TextMeshPro component
    public Animator playerAnimator; // Reference to the Animator component

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource; // Reference to the audio source component
    [SerializeField] private AudioClip firingSound; // Firing sound effect
    [SerializeField] private AudioClip emptyAmmoSound; // Empty ammo sound effect

    [Header("Firing Cooldown")]
    [SerializeField] private float fireCooldown = 1.0f;
    private float cooldownTimer = 0;
    private bool canFire = true;

    [Header("Cooldown UI")]
    [SerializeField] private Image cooldownEmptyImage;
    [SerializeField] private Image cooldownFilledImage;

    public int GetMaxAmmo()
    {
        return maxAmmo;
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    private void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!canFire)
        {
            UpdateCooldownTimer();
        }
    }

    private void UpdateCooldownTimer()
    {
        cooldownTimer -= Time.deltaTime;
        // Invert the fill amount logic
        cooldownFilledImage.fillAmount = 1 - (cooldownTimer / fireCooldown);

        if (cooldownTimer <= 0)
        {
            canFire = true;
            //cooldownFilledImage.gameObject.SetActive(false);
        }
    }

    public void FireProjectile()
    {
        if (currentAmmo <= 0)
        {
            Debug.LogWarning("No more projectiles left!");
            PlayEmptyAmmoSound(); // Play the empty ammo sound
            return;
        }

        if (!canFire)
        {
            Debug.LogWarning("Weapon is cooling down!");
            return;
        }

        if (spawnPoint == null || projectilePrefab == null)
        {
            Debug.LogWarning("Spawn point or projectile prefab not set");
            return;
        }

        // Check the conditions for climbing and the animator's state
        if (playerAnimator != null)
        {
            bool isClimbing = playerAnimator.GetBool("climbLedge");
            bool isInClimbState = playerAnimator.GetBool("ledgeClimbState");
            bool isAttacking = playerAnimator.GetBool("attack");
            bool isWallSliding = playerAnimator.GetBool("wallSlide");

            // Only play the attack animation if not climbing or in climb state
            if (!isClimbing && !isInClimbState && !isAttacking && !isWallSliding)
            {
                playerAnimator.Play("Weapon_Sumpit"); //the actual firing is in anim event using Fire()
            }
        }

        StartCoroutine(StartCooldown());
    }

    private IEnumerator StartCooldown()
    {
        canFire = false;
        cooldownTimer = fireCooldown;
        //cooldownFilledImage.gameObject.SetActive(true);
        cooldownFilledImage.fillAmount = 1; // Start full

        while (cooldownTimer > 0)
        {
            cooldownFilledImage.fillAmount = 1 - (cooldownTimer / fireCooldown);
            yield return null;
        }

        canFire = true;
        //cooldownFilledImage.gameObject.SetActive(false);
    }

    public void Fire()
    {
        GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
        projectile.GetComponent<Projectile_Sumpit>().ActivateProjectile(); // Activate the projectile
        UseAmmo();
        PlayFiringSound(); // Play the firing sound
    }

    private void PlayFiringSound()
    {
        if (audioSource != null && firingSound != null)
        {
            // Randomize pitch between a specified range, e.g., 0.9 and 1.1
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(firingSound);
        }
        else
        {
            Debug.LogWarning("AudioSource or firingSound not set");
        }
    }

    private void PlayEmptyAmmoSound()
    {
        if (audioSource != null && emptyAmmoSound != null)
        {
            audioSource.PlayOneShot(emptyAmmoSound);
        }
        else
        {
            Debug.LogWarning("AudioSource or emptyAmmoSound not set");
        }
    }

    private void UseAmmo()
    {
        currentAmmo--;
        UpdateAmmoUI();
    }

    public void AddAmmo(int amount)
    {
        currentAmmo = Mathf.Min(currentAmmo + amount, maxAmmo);
        UpdateAmmoUI();
    }

    private void UpdateAmmoUI()
    {
        ammoText.text = currentAmmo.ToString();
        
        if (currentAmmo == 0)
        {
            ammoText.color = Color.red; // Text color changes to red
        }
        else if (currentAmmo == maxAmmo)
        {
            ammoText.color = Color.green; // Text color changes to green
        }
        else
        {
            ammoText.color = Color.white; // Default color
        }
    }
}
