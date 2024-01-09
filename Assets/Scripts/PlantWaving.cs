using UnityEngine;

public class PlantWaving : MonoBehaviour
{
    // User-defined parameters for customization
    public float waveSpeed = 1f; // Speed of the waving effect
    public float waveHeight = 0.1f; // Height of the waving effect

    private float initialRotation;
    private float timeOffset; // To make the wave effect appear more natural by adding randomness

    // Start is called before the first frame update
    void Start()
    {
        initialRotation = transform.rotation.eulerAngles.z; // Store initial rotation
        timeOffset = Random.Range(0, 2 * Mathf.PI); // Initialize a random phase offset for the sine wave
    }

    // Update is called once per frame
    void Update()
    {
        WavePlant();
    }

    void WavePlant()
    {
        // Create a rotation effect based on a sine wave
        float wave = Mathf.Sin(Time.time * waveSpeed + timeOffset) * waveHeight; // Calculate the wave value
        Vector3 currentRotation = transform.rotation.eulerAngles; // Get current rotation
        currentRotation.z = initialRotation + wave; // Adjust the z-axis rotation
        transform.rotation = Quaternion.Euler(currentRotation); // Apply the new rotation
    }
}
