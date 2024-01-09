using System.Collections;
using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    public float knockbackStrength = 3f;
    public float knockbackDuration = 0.5f;
    public float verticalKnockbackStrength = 2f;
    public Transform groundCheck; // Transform for ground check
    public float groundCheckRadius = 0.5f;
    public LayerMask groundLayer; // Layer mask to identify the ground
    public float patrolResumeDelay = 1f; // Delay before resuming patrol

    private bool isKnockedBack = false;
    private float knockbackEndTime = 0f;
    private Vector2 knockbackDirection;
    private Rigidbody2D rb;
    private Patrol patrolScript;

    private Animator animator; // Animator component

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        patrolScript = GetComponent<Patrol>();
        animator = GetComponent<Animator>(); // Initialize Animator
    }

    void Update()
    {
        if (isKnockedBack && Time.time > knockbackEndTime)
        {
            if (IsGrounded())
            {
                StartCoroutine(ResumePatrolAfterDelay());
            }
        }
    }

    public void ApplyKnockback(Vector2 attackDirection)
    {
        isKnockedBack = true;
        knockbackDirection = attackDirection.normalized;
        knockbackEndTime = Time.time + knockbackDuration;

        knockbackDirection.y += verticalKnockbackStrength;

        if (patrolScript != null)
        {
            patrolScript.enabled = false;
        }

        StartCoroutine(KnockbackCoroutine());
    }

    IEnumerator KnockbackCoroutine()
    {
        float timer = 0f;
        while (timer < knockbackDuration)
        {
            rb.velocity = knockbackDirection * knockbackStrength;
            timer += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero;
    }

    private IEnumerator ResumePatrolAfterDelay()
    {
        // Trigger the "Stunned" animation
        if (animator != null)
        {
            animator.SetBool("IsStunned", true);
        }

        yield return new WaitForSeconds(patrolResumeDelay);

        if (IsGrounded())
        {
            EndKnockback();
        }
    }

    private void EndKnockback()
    {
        isKnockedBack = false;

        // Stop the "Stunned" animation
        if (animator != null)
        {
            animator.SetBool("IsStunned", false);
        }

        if (patrolScript != null)
        {
            patrolScript.enabled = true;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
}
