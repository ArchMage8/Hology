using System.Collections;
using TMPro;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class ClockSystem : MonoBehaviour
{
    public static ClockSystem Instance { get; private set; }

    [Header("Clock Stuffs")]

    public TextMeshProUGUI hoursText;
    public TextMeshProUGUI minutesText;
    public TextMeshProUGUI colonText;
    public bool TimerEndBool = false;


    [Space(10)]
    [Header("Audio")]
    public SFXManager_Exception AlarmSoundPlayer;
    public AudioClip ClockEndSound;

    [Space(10)]
    public Animator BackgroundSkyAnimator;



    private int hours = 9;
    private int minutes = 0;

    private int minuteCounter = 0;
    private bool isClockRunning = false;

    private bool colonVisible = true;
    private bool NoonDone = false;
    private bool AfterNoonDone = false;
    private bool DuskDone = false;

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

    private void Update()
    {
        if(hours == 12 && !NoonDone)
        {
            NoonDone = true;
            BackgroundSkyAnimator.SetTrigger("MorningToNoon" + "");
        }
        
        if(hours == 16 && !AfterNoonDone)
        {
            AfterNoonDone = true;
            BackgroundSkyAnimator.SetTrigger("Afternoon");
        }

        if(hours == 17 && !DuskDone)
        {
            DuskDone = true;
            BackgroundSkyAnimator.SetTrigger("Dusk");
        }
    }

    public void StartClock()
    {
        if (!isClockRunning)
        {
            isClockRunning = true;
            StartCoroutine(BlinkingColon());
            NPC_Main.Instance.StartSystem();
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
