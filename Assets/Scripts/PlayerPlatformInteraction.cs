using UnityEngine;

public class PlayerPlatformInteraction : MonoBehaviour
{
    private Rigidbody2D playerRigidbody;
    private Transform platformTransform;
    private bool isOnPlatform = false;

    private Movement movementScript;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        movementScript = GetComponent<Movement>();
    }

    private void Update()
    {
        if (isOnPlatform && playerRigidbody.velocity.magnitude > 0.1f) // Player is moving actively
        {
            DetachFromPlatform();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            platformTransform = collision.transform;
            isOnPlatform = true;
            AttachToPlatform();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform") && !isOnPlatform && playerRigidbody.velocity.magnitude <= 0.1f)
        {
            AttachToPlatform();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            isOnPlatform = false;
            DetachFromPlatform();
        }
    }

    private void AttachToPlatform()
    {
        transform.parent = platformTransform;
        isOnPlatform = true;
    }

    private void DetachFromPlatform()
    {
        GameObject playerHolder = GameObject.Find("PlayerHolder");
        if (playerHolder != null)
        {
            transform.parent = playerHolder.transform;
        }
        else
        {
            Debug.LogError("PlayerHolder not found in the scene!");
        }
        isOnPlatform = false;
    }
}
