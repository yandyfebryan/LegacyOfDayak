using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private CheckpointManager checkpointManager; // Reference set in the Inspector
    [SerializeField] private GameObject checkpointText; // Reference to the CheckpointText GameObject
    [SerializeField] private AudioClip checkpointSound; // Optional audio clip for the checkpoint
    [SerializeField] private AudioSource audioSource; // AudioSource to play the sound
    private Animator animator; // Animator for the checkpoint
    private bool isCheckpointActivated = false; // Flag to ensure activation happens only once

    public int seconds = 2;

    void Start()
    {
        animator = GetComponent<Animator>(); // Get the Animator component
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component

        // If there's no AudioSource on this GameObject, add one
        if (audioSource == null && checkpointSound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && checkpointManager != null && !isCheckpointActivated)
        {
            checkpointManager.SetLastCheckpoint(transform.position);
            Debug.Log("Checkpoint set at: " + transform.position);

            if (animator != null)
            {
                animator.SetTrigger("Lit"); // Trigger the 'Lit' animation
            }
            else
            {
                Debug.LogError("Animator not found on the checkpoint object");
            }

            if (checkpointSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(checkpointSound); // Play the checkpoint sound only once
            }

            if (checkpointText != null)
            {
                StartCoroutine(ShowCheckpointText());
            }
            else
            {
                Debug.LogError("CheckpointText GameObject not assigned");
            }

            isCheckpointActivated = true; // Set the flag to true to prevent reactivation
        }
    }

    IEnumerator ShowCheckpointText()
    {
        checkpointText.SetActive(true); // Enable the CheckpointText GameObject
        yield return new WaitForSeconds(seconds); // Wait for 3 seconds
        checkpointText.SetActive(false); // Disable the CheckpointText GameObject
    }
}
