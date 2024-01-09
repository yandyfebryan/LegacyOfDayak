using UnityEngine;

public class StarScorer : MonoBehaviour
{
    public GameObject[] filledStars; 
    
    public Timer levelTimer;
    public PlayerHealth playerHealth;
    public float timeLimitForThirdStar = 120f; // Time limit (in seconds) set in the editor for the third star
    
    private void Start()
    {
        foreach (GameObject star in filledStars)
        {
            star.SetActive(false);
        }
    }

    public void LevelCompleted()
    {
        float healthPercentage = (playerHealth.CurrentHealth / playerHealth.MaxHealth) * 100f;
        UpdateStarScore(healthPercentage);
    }

    private void UpdateStarScore(float healthPercentage)
    {
        filledStars[0].SetActive(true); // Always activate the first star

        if (healthPercentage > 50)
        {
            filledStars[1].SetActive(true);
        }
        else
        {
            filledStars[1].SetActive(false);
        }

        if (levelTimer.elapsedTime <= timeLimitForThirdStar)
        {
            filledStars[2].SetActive(true);
        }
        else
        {
            filledStars[2].SetActive(false);
        }
    }
}
