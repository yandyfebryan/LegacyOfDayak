using UnityEngine;
using UnityEngine.UI;

public class UpdateUITimer : MonoBehaviour
{
    public Timer timer;  // Reference to the Timer script
    public Text timeText;  // Reference to the UI Text component

    private void Update()
    {
        // Convert the elapsed time to minutes:seconds format
        int minutes = Mathf.FloorToInt(timer.elapsedTime / 60);
        int seconds = Mathf.FloorToInt(timer.elapsedTime % 60);

        // Update the UI Text
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
