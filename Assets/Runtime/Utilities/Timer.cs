using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    private float timer = 0f;
    private bool isRunning = false;

    private void Awake() => ValidateRequiredVariables();

    private void OnEnable() => StartTimer();

    // Function to start the timer
    public void StartTimer()
    {
        isRunning = true;
        timer = 0f; // Reset the timer when starting
    }

    // Function to stop the timer
    public void StopTimer() => isRunning = false;

    // Function to get the current timer time in seconds
    public float GetTimerTime()
    {
        return timer;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            timer += Time.deltaTime;
            DisplayTime(timer);
        }
    }

    // Function to display time in minutes:seconds format
    void DisplayTime(float timeToDisplay)
    {
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void ValidateRequiredVariables()
    {
        if (timerText == null) { Debug.LogError("Null References: " + timerText.name); }
    }
}