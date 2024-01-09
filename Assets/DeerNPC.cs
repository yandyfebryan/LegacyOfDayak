using UnityEngine;

public class DeerNPC : MonoBehaviour
{
    private enum State { Idle, Walking, Grazing, Stunned }
    private State currentState = State.Idle;

    public float moveSpeed = 2f;
    public float grazingTime = 5f;
    public float idleTime = 3f;
    public float walkingTime = 5f;
    public float stunnedTime = 2f; // Time the deer remains stunned
    public float stunCooldownTime = 3f; // Cooldown time after being stunned
    public LayerMask obstacleLayer;

    private Animator animator;
    private float stateTimer;
    private float stunCooldownTimer;
    private Vector2 movementDirection;
    private float directionChangeCooldown = 0f;

    private EnemyHealth enemyHealth; // Reference to the EnemyHealth script

    private void Start()
    {
        animator = GetComponent<Animator>();
        movementDirection = Vector2.right; // Initial movement direction
        enemyHealth = GetComponent<EnemyHealth>(); // Get the EnemyHealth component
    }

    private void Update()
    {
        if (directionChangeCooldown > 0)
        {
            directionChangeCooldown -= Time.deltaTime;
        }

        if (stunCooldownTimer > 0)
        {
            stunCooldownTimer -= Time.deltaTime;
        }

        // Check if the deer is stunned
        if (enemyHealth.RecentlyDamaged && currentState != State.Stunned && stunCooldownTimer <= 0)
        {
            ChangeState(State.Stunned);
        }

        switch (currentState)
        {
            case State.Idle:
                HandleIdle();
                break;
            case State.Walking:
                HandleWalking();
                break;
            case State.Grazing:
                HandleGrazing();
                break;
            case State.Stunned:
                HandleStunned();
                break;
        }
    }

    private void ChangeState(State newState)
    {
        currentState = newState;
        stateTimer = 0;
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        switch (currentState)
        {
            case State.Idle:
                animator.Play("Deer_Idle");
                break;
            case State.Walking:
                animator.Play("Deer_Running");
                break;
            case State.Grazing:
                animator.Play("Deer_Grazing");
                break;
            case State.Stunned:
                animator.Play("Deer_Stunned");
                break;
        }
    }

    private void HandleIdle()
    {
        stateTimer += Time.deltaTime;
        if (stateTimer > idleTime)
        {
            ChangeState(State.Walking);
        }
    }

    private void HandleWalking()
    {
        stateTimer += Time.deltaTime;

        // Move the deer
        transform.Translate(movementDirection * moveSpeed * Time.deltaTime);

        // Check for ground ahead
        RaycastHit2D groundInfo = Physics2D.Raycast(transform.position, Vector2.down, 1f, obstacleLayer);
        if (groundInfo.collider == null && directionChangeCooldown <= 0)
        {
            TurnAround();
        }

        // Check for obstacles in front
        RaycastHit2D obstacleInfo = Physics2D.Raycast(transform.position, movementDirection, 1f, obstacleLayer);
        if (obstacleInfo.collider != null && directionChangeCooldown <= 0)
        {
            TurnAround();
        }

        // Change state after a period of walking
        if (stateTimer > walkingTime)
        {
            ChangeState(Random.Range(0, 2) == 0 ? State.Idle : State.Grazing);
        }
    }

    private void HandleStunned()
    {
        stateTimer += Time.deltaTime;
        if (stateTimer > stunnedTime)
        {
            ChangeState(State.Idle); // Change back to Idle after being stunned
            stunCooldownTimer = stunCooldownTime; // Start the stun cooldown
        }
    }

    private void TurnAround()
    {
        movementDirection = -movementDirection;
        directionChangeCooldown = 1f; // Add a cooldown to prevent immediate turning back
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z); // Flip the deer
    }

    private void HandleGrazing()
    {
        stateTimer += Time.deltaTime;
        if (stateTimer > grazingTime)
        {
            ChangeState(State.Idle);
        }
    }
}
