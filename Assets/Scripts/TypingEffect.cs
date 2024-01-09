using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TypingEffect : MonoBehaviour
{
    [TextArea]
    public string fullText;
    public float delay = 0.05f; // Time delay between characters
    private TextMeshProUGUI displayText;

    private void Start()
    {
        displayText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            displayText.text = fullText.Substring(0, i);
            yield return new WaitForSeconds(delay);
        }
    }

    // If you want to reset and start the typing effect again, call this function
    public void ResetAndTypeAgain()
    {
        StopAllCoroutines();
        displayText.text = "";
        StartCoroutine(ShowText());
    }
}
