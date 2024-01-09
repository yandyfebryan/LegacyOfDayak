using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{
    public float floatStrength = 1f; // Adjust this to change how high the platform floats
    public float speed = 1f; // Adjust this to change the speed of the floating movement
    private Vector2 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        Vector2 floatY = originalPosition;
        // Using Mathf.Sin with Time.time multiplied by speed for faster or slower movement
        floatY.y += Mathf.Sin(Time.time * speed) * floatStrength;
        transform.position = floatY;
    }
}
