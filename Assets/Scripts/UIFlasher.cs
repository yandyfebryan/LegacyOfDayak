using UnityEngine;
using System.Collections;
using System.Collections.Generic; // For using List

public class UIFlasher : MonoBehaviour
{
    public List<CanvasGroup> canvasGroups; // List of CanvasGroups
    public float flashDuration = 0.5f; // Duration for one complete flash

    private void Start()
    {
        // Initialize each canvasGroup in the list if not set
        for (int i = 0; i < canvasGroups.Count; i++)
        {
            if (canvasGroups[i] == null)
            {
                canvasGroups[i] = GetComponent<CanvasGroup>();
            }
        }

        StartCoroutine(Flash());
    }

    private IEnumerator Flash()
    {
        while (true) // Loop indefinitely
        {
            foreach (CanvasGroup canvasGroup in canvasGroups)
            {
                StartCoroutine(FadeTo(canvasGroup, 1, flashDuration / 2)); // Fade in
            }
            yield return new WaitForSeconds(flashDuration / 2);

            foreach (CanvasGroup canvasGroup in canvasGroups)
            {
                StartCoroutine(FadeTo(canvasGroup, 0, flashDuration / 2)); // Fade out
            }
            yield return new WaitForSeconds(flashDuration / 2);
        }
    }

    private IEnumerator FadeTo(CanvasGroup canvasGroup, float targetAlpha, float duration)
    {
        float startAlpha = canvasGroup.alpha;
        float time = 0;

        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = targetAlpha; // Ensure target alpha is set
    }
}
