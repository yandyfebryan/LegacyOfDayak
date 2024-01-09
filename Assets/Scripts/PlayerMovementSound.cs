using UnityEngine;

public class PlayerMovementSound : MonoBehaviour
{
    [SerializeField]
    private Movement movementComponent;

    [SerializeField]
    private AudioSource walkingSound;

    [SerializeField]
    private CollisionSenses collisionSenses;

    private void Update()
    {
        HandleWalkingSound();
    }

    private void HandleWalkingSound()
    {
        if (movementComponent.CurrentVelocity.x != 0 && collisionSenses.Ground)
        {
            if (!walkingSound.isPlaying)
            {
                walkingSound.Play();
            }
        }
        else
        {
            if (walkingSound.isPlaying)
            {
                walkingSound.Stop();
            }
        }
    }
}
