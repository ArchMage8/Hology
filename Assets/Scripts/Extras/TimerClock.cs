using UnityEngine;
using System.Collections;
using TMPro;

public class TimerClock : MonoBehaviour
{
    public TextMeshProUGUI clockText;
    private float timer = 0f;
    private int hours = 9;
    private int minutes = 0;
    private bool isClockRunning = false;
    private bool isColonVisible = true;

    private float realSecondsPerGameHour = 60f;
    private float timeStep = 15f;
    private float secondsPerGameMinute;
    private float colonToggleTimer = 0f;

    void Start()
    {
        secondsPerGameMinute = realSecondsPerGameHour / 60f;
        UpdateClockUI();
    }

    void Update()
    {
        if (!isClockRunning) return;

        timer += Time.deltaTime;
        colonToggleTimer += Time.deltaTime;

        // Call UpdateClock and UpdateClockUI every 15 seconds
        if (timer >= 15f) // Changed to 15 seconds
        {
            UpdateClock();
            UpdateClockUI();
            timer = 0f; // Reset the timer
        }

        // Toggle colon visibility every 1 second
        if (colonToggleTimer >= 1f)
        {
            isColonVisible = !isColonVisible;
            UpdateClockUI(); // Update the UI for colon visibility
            colonToggleTimer = 0f; // Reset the colon toggle timer
        }
    }

    public void StartClock()
    {
        isClockRunning = true;
    }

    private void UpdateClock()
    {
        minutes += 15;

        if (minutes >= 60)
        {
            minutes = 0;
            hours++;
        }

        if (hours >= 17 && minutes == 0)
        {
            isClockRunning = false;
            OnClockEnd();
        }
    }

    private void OnClockEnd()
    {
        Debug.Log("Clock has reached 17:00");
        PaperManager.Instance.timerExpired = true;
    }

    private void UpdateClockUI()
    {
        string colon = isColonVisible ? ":" : " ";
        clockText.text = string.Format("{0:D2}{1}{2:D2}", hours, colon, minutes);
    }
}
