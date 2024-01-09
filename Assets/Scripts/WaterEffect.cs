using UnityEngine;

public class WaterEffect : MonoBehaviour
{
    public float waveFrequency = 1f;
    public float waveHeight = 0.5f;
    public bool oscillateVertical = true;

    private Vector3 originalScale;
    private Vector3 originalPosition;

    void Start()
    {
        originalScale = transform.localScale;
        originalPosition = transform.position;
    }

    void Update()
    {
        if (oscillateVertical)
        {
            // Oscillate the position for vertical wave motion
            float newY = originalPosition.y + Mathf.Sin(Time.time * waveFrequency) * waveHeight;
            transform.position = new Vector3(originalPosition.x, newY, originalPosition.z);
        }
        else
        {
            // Oscillate the scale for horizontal wave motion
            float newScaleY = originalScale.y + Mathf.Sin(Time.time * waveFrequency) * waveHeight;
            transform.localScale = new Vector3(originalScale.x, newScaleY, originalScale.z);
        }
    }
}
