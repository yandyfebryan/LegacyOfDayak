using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] private bool enableVerticalMovement = true;
    [SerializeField] private bool enableHorizontalMovement = true;
    [SerializeField] private bool enableSpriteFlip = true; // New field to control sprite flipping
    [SerializeField] private float patrolDistanceVertical = 2f;
    [SerializeField] private float patrolDistanceHorizontal = 4f;
    [SerializeField] private float patrolSpeedVertical = 1f;
    [SerializeField] private float patrolSpeedHorizontal = 2f;

    private Rigidbody2D rb2D;
    private Vector2 startPosition;
    private Vector2 endPositionVertical;
    private Vector2 endPositionHorizontal;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        startPosition = rb2D.position;
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Calculate the end positions based on the patrol distances
        endPositionVertical = startPosition + Vector2.up * patrolDistanceVertical * Mathf.Sign(patrolSpeedVertical);
        endPositionHorizontal = startPosition + Vector2.right * patrolDistanceHorizontal * Mathf.Sign(patrolSpeedHorizontal);
    }

    void Update()
    {
        if (enableVerticalMovement)
        {
            Move(ref endPositionVertical, ref patrolSpeedVertical);
        }

        if (enableHorizontalMovement)
        {
            Move(ref endPositionHorizontal, ref patrolSpeedHorizontal);
            if (enableSpriteFlip) // Check if sprite flipping is enabled
            {
                FlipSprite();
            }
        }
    }

    private void Move(ref Vector2 endPosition, ref float patrolSpeed)
    {
        rb2D.position = Vector2.MoveTowards(rb2D.position, endPosition, Mathf.Abs(patrolSpeed) * Time.deltaTime);

        // If the enemy reaches the end position, reverse the direction and swap the start and end positions
        if (rb2D.position == endPosition)
        {
            patrolSpeed = -patrolSpeed;
            Vector2 temp = startPosition;
            startPosition = endPosition;
            endPosition = temp;
        }
    }

    private void FlipSprite()
    {
        if (patrolSpeedHorizontal > 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
