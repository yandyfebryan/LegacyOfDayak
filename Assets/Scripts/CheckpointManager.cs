using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private Vector3 lastCheckpointPosition;

    [SerializeField] private GameObject playerHolder; // Reference to the PlayerHolder object
    [SerializeField] private PlayerHealth playerHealth; // Reference to the PlayerHealth script

    // Removed the Start method as it's no longer needed

    public void SetLastCheckpoint(Vector3 checkpointPosition)
    {
        // Adjust the x position by -2 for all checkpoints
        checkpointPosition.x -= 2;

        lastCheckpointPosition = checkpointPosition;
        Debug.Log("New checkpoint set at: " + checkpointPosition);
    }

    public Vector3 GetLastCheckpointPosition()
    {
        return lastCheckpointPosition;
    }

    public void RespawnPlayer()
    {
        Debug.Log("Respawning player at: " + lastCheckpointPosition);

        // Move the PlayerHolder to the spawn position
        playerHolder.transform.position = lastCheckpointPosition;

        // Reset the local position of the player within the PlayerHolder
        if (playerHealth != null)
        {
            playerHealth.transform.localPosition = Vector3.zero; // Reset local position
            playerHealth.gameObject.SetActive(true); 
            playerHealth.ResetHealthToFull(); 
        }
        else
        {
            Debug.LogError("PlayerHealth script not found.");
        }
    }
}
