using UnityEngine;

public class Timer : MonoBehaviour
{
    public float elapsedTime = 0f;
    private bool isRunning = true;

    private void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
        }
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    // New methods for pausing and resuming the timer
    public void PauseTimer()
    {
        isRunning = false;
    }

    public void ResumeTimer()
    {
        isRunning = true;
    }
}
