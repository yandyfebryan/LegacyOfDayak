using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MainMenuMusicController : MonoBehaviour
{
    private AudioSource audioSource;
    public float pauseDuration = 2.0f; 
    public float fadeOutDuration = 2.0f;  // Duration of the fade out effect

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        PlayMusic();
    }

    private void PlayMusic()
    {
        audioSource.volume = 1.0f; // Reset volume to full before playing again
        audioSource.Play();
        StartCoroutine(FadeOutAndPauseBeforeLoop());
    }

    private IEnumerator FadeOutAndPauseBeforeLoop()
    {
        // Wait until near the end of the clip, minus fade out duration
        yield return new WaitForSeconds(audioSource.clip.length - fadeOutDuration);

        // Now start the fade out
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeOutDuration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume; // Resetting volume for next time

        // Wait for the desired pause duration
        yield return new WaitForSeconds(pauseDuration);

        // Play the music again
        PlayMusic();
    }
}
