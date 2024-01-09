using UnityEngine;
using UnityEngine.Audio;

public class PlayAudioOnAnimation : MonoBehaviour
{
    public AudioClip audioClip; // Assign this in the inspector
    public AudioMixerGroup audioMixerGroup; // Assign this in the inspector
    public float volume = 1.0f; // Default volume, can be adjusted in inspector

    private AudioSource audioSource;

    private void Awake()
    {
        // Check if there's an AudioSource on this GameObject, if not, add one
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Set up AudioSource
        audioSource.outputAudioMixerGroup = audioMixerGroup;
        audioSource.playOnAwake = false;
    }

    public void PlayAudio()
    {
        if (audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.volume = volume;

            // Set a random pitch
            audioSource.pitch = Random.Range(0.5f, 1.5f);

            // Play the audio clip
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Audio clip not assigned on " + gameObject.name);
        }
    }
}
