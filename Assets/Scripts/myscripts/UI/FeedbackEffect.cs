using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class FeedbackEffect : MonoBehaviour
{
    [SerializeField] private AudioSource heartSoundEffect;
    [SerializeField] private TMP_Text healthAmountText;
    [SerializeField] private TMP_Text ammoAmountText;
    [SerializeField] private TMP_Text woodAmountText;

    public float moveSpeed = 0.8f;
    public float fadeSpeed = 1f;
    public float duration = 1f;

    private CanvasGroup canvasGroup;
    private float timer;
    public Transform playerTransform;

    private static List<FeedbackEffect> activeEffects = new List<FeedbackEffect>();
    private Vector3 offset;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        offset = Vector3.up * activeEffects.Count; // Initial offset based on the number of active effects
    }

    private void Update()
    {
        if (timer < duration)
        {
            timer += Time.deltaTime;
            offset += Vector3.up * moveSpeed * Time.deltaTime; // Move the effect upwards
            transform.position = playerTransform.position + offset; // Update position relative to the player
        }
        else
        {
            gameObject.SetActive(false);
            activeEffects.Remove(this);
        }
    }

    public void Show()
    {
        offset = Vector3.up * activeEffects.Count; // Reset offset for this new effect
        timer = 0;
        gameObject.SetActive(true);
        heartSoundEffect.Play();
        StartCoroutine(FadeInOut());

        activeEffects.Add(this);
    }

    public void ShowWithHealthAmount(int healthAmount)
    {
        healthAmountText.text = $"+{healthAmount}";
        Show();
    }

    public void ShowWithAmmoAmount(int ammoAmount)
    {
        ammoAmountText.text = $"+{ammoAmount}";
        Show();
    }

    public void ShowWithWoodAmount(int woodAmount)
    {
        if (woodAmountText != null)
        {
            woodAmountText.text = $"+{woodAmount}";
            Show();
        }
    }

    private IEnumerator FadeInOut()
    {
        float fadeTimer = 0;

        while (fadeTimer < duration / 2)
        {
            fadeTimer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, fadeTimer * fadeSpeed / (duration / 2));
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        fadeTimer = 0;

        while (fadeTimer < duration / 2)
        {
            fadeTimer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0, fadeTimer * fadeSpeed / (duration / 2));
            yield return null;
        }

        if (activeEffects.Contains(this))
        {
            activeEffects.Remove(this);
        }
    }

    private void OnDisable()
    {
        if (activeEffects.Contains(this))
        {
            activeEffects.Remove(this);
        }
    }
}
