using UnityEngine;
using UnityEngine.Audio;

public class Plants : MonoBehaviour
{
    public float waveSpeed = 1f;
    public float waveHeight = 1f;
    public int health = 1;
    public GameObject plantDestroyed;
    
    public GameObject heartPrefab;
    public GameObject ammoPrefab;
    public float spawnChance = 0.15f;
    public float ammoSpawnChance = 0.15f;

    private float initialRotation;
    private bool isHit = false;
    private float hitTime = 0.5f;
    private float hitTimer = 0f;
    private Animator animator;

    private RangedAttack playerRangedAttack;
    private PlayerHealth playerHealth;

    [Header("Audio")]
    [SerializeField] private AudioClip plantDestructionSound; // Plant destruction sound effect
    [SerializeField] private AudioMixerGroup outputMixerGroup; // Output audio mixer group
    [SerializeField] private float destructionSoundVolume = 0.8f; // Adjustable volume

    void Start()
    {
        initialRotation = transform.rotation.eulerAngles.z;
        animator = GetComponent<Animator>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerRangedAttack = player.GetComponent<RangedAttack>();
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    void Update()
    {
        if (isHit)
        {
            WavePlant();
            hitTimer -= Time.deltaTime;
            if (hitTimer <= 0f)
            {
                isHit = false;
                ResetPlantRotation();
            }
        }
    }

    void WavePlant()
    {
        float wave = Mathf.Sin(Time.time * waveSpeed) * waveHeight;
        Vector3 currentRotation = transform.rotation.eulerAngles;
        currentRotation.z = initialRotation + wave;
        transform.rotation = Quaternion.Euler(currentRotation);
    }

    void ResetPlantRotation()
    {
        Vector3 currentRotation = transform.rotation.eulerAngles;
        currentRotation.z = initialRotation;
        transform.rotation = Quaternion.Euler(currentRotation);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Weapon"))
        {
            isHit = true;
            hitTimer = hitTime;
            TakeDamage();
        }
    }

   void TakeDamage()
    {
        health -= 1;
        if (health <= 0)
        {
            PlayDestructionSound(); // Play the destruction sound
            PlayDeathAnimation();
            DecideAndSpawnItem();
        }
    }

   private void PlayDestructionSound()
    {
        if (plantDestructionSound != null)
        {
            AudioSource tempAudioSource = gameObject.AddComponent<AudioSource>();
            tempAudioSource.clip = plantDestructionSound;
            tempAudioSource.playOnAwake = false;
            tempAudioSource.volume = destructionSoundVolume;
            tempAudioSource.outputAudioMixerGroup = outputMixerGroup;

            // Randomize pitch slightly for variation
            tempAudioSource.pitch = Random.Range(0.90f, 1.05f);

            tempAudioSource.Play();

            Destroy(tempAudioSource, plantDestructionSound.length);
        }
        else
        {
            Debug.LogWarning("plantDestructionSound not set");
        }
    }

    void DecideAndSpawnItem()
    {
        bool shouldSpawnAmmo = ShouldSpawnAmmo();
        bool shouldSpawnHeart = ShouldSpawnHeart();

        if (shouldSpawnAmmo && !shouldSpawnHeart)
        {
            TrySpawnAmmo();
        }
        else if (!shouldSpawnAmmo && shouldSpawnHeart)
        {
            TrySpawnHeart();
        }
        else if (shouldSpawnAmmo && shouldSpawnHeart)
        {
            if (Random.value < 0.5f) TrySpawnHeart();
            else TrySpawnAmmo();
        }
    }

    bool ShouldSpawnAmmo()
    {
        if (playerRangedAttack != null)
        {
            int halfMaxAmmo = playerRangedAttack.GetMaxAmmo() / 2;
            if (playerRangedAttack.GetCurrentAmmo() < halfMaxAmmo)
            {
                return Random.value < (ammoSpawnChance * 1.5f);
            }
        }
        return Random.value < ammoSpawnChance;
    }

    bool ShouldSpawnHeart()
    {
        if (playerHealth != null)
        {
            float halfMaxHealth = playerHealth.MaxHealth / 2f;
            if (playerHealth.CurrentHealth < halfMaxHealth)
            {
                return Random.value < (spawnChance * 1.5f); // Increased spawn chance
            }
        }
        return Random.value < spawnChance;
    }

    void TrySpawnHeart()
    {
        if (Random.value < spawnChance)
        {
            Instantiate(heartPrefab, transform.position, Quaternion.identity);
        }
    }

    void TrySpawnAmmo()
    {
        if (Random.value < ammoSpawnChance)
        {
            Instantiate(ammoPrefab, transform.position, Quaternion.identity);
        }
    }

    void PlayDeathAnimation()
    {
        animator.SetTrigger("Plant");
        GetComponent<Collider2D>().enabled = false;
    }

    public void Destroy()
    {
        // If you want to completely destroy the object
        Destroy(gameObject);
    }

    void ActivateDestroyedState()
    {
        plantDestroyed.SetActive(true);
    }
}
