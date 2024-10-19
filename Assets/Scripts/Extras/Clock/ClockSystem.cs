using System.Collections;
using TMPro;
using UnityEngine;

public class ClockSystem : MonoBehaviour
{
    public static ClockSystem Instance { get; private set; }

    [Header("Clock Stuffs")]

    public TextMeshProUGUI hoursText;
    public TextMeshProUGUI minutesText;
    public TextMeshProUGUI colonText;

    [Space(10)]
    [Header("Audio")]
    public SFXManager_Exception AlarmSoundPlayer;
    public AudioClip ClockEndSound;

    public bool TimerEndBool = false;

    private int hours = 9;
    private int minutes = 0;

    private int minuteCounter = 0;
    private bool isClockRunning = false;

    private bool colonVisible = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateTimeDisplay();
    }

    public void StartClock()
    {
        if (!isClockRunning)
        {
            isClockRunning = true;
            StartCoroutine(BlinkingColon());
            StartCoroutine(UpdateClock());
        }
    }

    private IEnumerator UpdateClock()
    {
        while (isClockRunning)
        {
            yield return new WaitForSeconds(15);
            AddMinutes(15);
        }
    }

    private void AddMinutes(int amount)
    {
        minutes += amount;
        minuteCounter++;

        if (minuteCounter == 4)
        {
            minuteCounter = 0;
            minutes = 0;
            AddHours(1);
        }

        UpdateTimeDisplay();
    }

    private void AddHours(int amount)
    {
        hours += amount;

        if (hours == 17)
        {
            TimerEnd();
            isClockRunning = false;
        }
    }

    private void UpdateTimeDisplay()
    {
        hoursText.text = hours.ToString("00");
        minutesText.text = minutes.ToString("00");
    }

    private IEnumerator BlinkingColon()
    {
        while (true)
        {
            colonVisible = !colonVisible;
            colonText.gameObject.SetActive(colonVisible);
            yield return new WaitForSeconds(1);
        }
    }

    private void TimerEnd()
    {
        //Debug.Log("Timer has reached the end.");
        AlarmSoundPlayer.PlaySound(ClockEndSound);
        TimerEndBool = true;
    }
}
