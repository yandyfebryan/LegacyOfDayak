using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{
    public LevelManager levelManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is the player
        if (other.gameObject.CompareTag("Player"))
        {
            // Call the method in LevelManager to handle the level completion
            levelManager.HandleLevelComplete();
        }
    }
}
