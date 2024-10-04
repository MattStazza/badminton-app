using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    private float timer = 0f;
    private bool isRunning = false;
    private string formatTime;

    private void Awake() => ValidateRequiredVariables();

    private void OnEnable() => StartTimer();


    public void StartTimer()
    {
        isRunning = true;
        timer = 0f;
    }

    public void StopTimer() => isRunning = false;

    public string GetFormatTime() { return formatTime; }

    private string FormatTime(float timeToDisplay)
    {
        string formattedTime;
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);
        formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);

        return formattedTime;
    }


    void Update()
    {
        if (isRunning)
        {
            timer += Time.deltaTime;
            formatTime = FormatTime(timer);
            timerText.text = formatTime;
        }
    }



    private void ValidateRequiredVariables()
    {
        if (timerText == null) { Debug.LogError("Null References: " + timerText.name); }
    }
}