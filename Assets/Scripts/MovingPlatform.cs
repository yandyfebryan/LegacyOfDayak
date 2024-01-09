using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private GameObject startPointObject;
    [SerializeField] private GameObject endPointObject;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float bobbingAmplitude = 0.5f;
    [SerializeField] private float bobbingFrequency = 1f;

    private Vector2 startPoint;
    private Vector2 endPoint;
    private bool isMovingToEnd = true;
    private float originalY;

    private void Start()
    {
        if (startPointObject != null && endPointObject != null)
        {
            startPoint = startPointObject.transform.position;
            endPoint = endPointObject.transform.position;
            originalY = transform.position.y;
        }
        else
        {
            Debug.LogError("Start point and end point objects must be set.");
            this.enabled = false; // Disable script if start or end point objects are not set
        }
    }

    private void Update()
    {
        MovePlatform();
        BobbingEffect();
    }

    private void MovePlatform()
    {
        Vector2 target = isMovingToEnd ? endPoint : startPoint;
        Vector2 horizontalMovement = Vector2.MoveTowards(new Vector2(transform.position.x, originalY), target, speed * Time.deltaTime);
        
        // Update the position with both horizontal and vertical (bobbing) components
        transform.position = new Vector2(horizontalMovement.x, transform.position.y);

        // Check if the platform has reached the horizontal target
        if (Mathf.Approximately(transform.position.x, target.x))
        {
            isMovingToEnd = !isMovingToEnd; // Toggle the direction of movement
        }
    }

    private void BobbingEffect()
    {
        float newY = originalY + Mathf.Sin(Time.time * bobbingFrequency) * bobbingAmplitude;
        transform.position = new Vector2(transform.position.x, newY);
    }
}
