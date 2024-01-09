using UnityEngine;
using TMPro; // Include TextMeshPro namespace

public class ObjectiveTracker : MonoBehaviour
{
    public GameObject[] enemies;

    // Add a TextMeshProUGUI reference for displaying the number of enemies left
    public TextMeshProUGUI enemiesLeftText;

    public delegate void ObjectiveCompletedAction();
    public event ObjectiveCompletedAction OnObjectiveCompleted;

    void Start()
    {
        UpdateEnemyCountDisplay(); // Update the display at the start
    }

    void Update()
    {
        CheckEnemies();
    }

    private void CheckEnemies()
    {
        int aliveEnemies = 0;
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                aliveEnemies++;
            }
        }

        UpdateEnemyCountDisplay(aliveEnemies); // Update the display each time CheckEnemies is called

        if (aliveEnemies == 0)
        {
            ObjectiveCompleted();
        }
    }

    private void UpdateEnemyCountDisplay(int count = 0)
    {
        if (enemiesLeftText != null)
        {
            enemiesLeftText.text = $"Enemies Left: {count}";
        }
    }

    private void ObjectiveCompleted()
    {
        if (OnObjectiveCompleted != null)
        {
            OnObjectiveCompleted();
        }
    }
}
