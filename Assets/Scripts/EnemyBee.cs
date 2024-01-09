using UnityEngine;

public class EnemyBee : MonoBehaviour
{
    public float speed = 5.0f;
    public float detectionRange = 5.0f;
    public LayerMask obstaclesLayer;
    private Transform playerTransform;
    private bool isPlayerInRange = false;

    void Start()
    {
        // Find the player by tag or another method
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (isPlayerInRange && ClearLineOfSight())
        {
            ChasePlayer();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == playerTransform)
        {
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform == playerTransform)
        {
            isPlayerInRange = false;
        }
    }

    void ChasePlayer()
    {
        // Move towards the player
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
    }

    bool ClearLineOfSight()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, playerTransform.position - transform.position, detectionRange, obstaclesLayer);
        return hit.collider == null || hit.transform == playerTransform;
    }
}
