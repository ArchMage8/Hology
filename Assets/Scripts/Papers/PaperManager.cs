using UnityEngine;
using System.Collections;
using TMPro;

public class PaperManager : MonoBehaviour
{
    public static PaperManager Instance { get; private set; }

    [Header("Paper Objects")]
    public GameObject[] papers;  // Array of gameobjects with PaperProperties

    [Header("Delay Settings")]
    public float exitDelay = 2.0f;  // Delay before current paper is disabled
    public float nextPaperDelay = 1.0f;  // Delay before next paper is enabled

    [Header("Outcome Settings")]
    public int maxIncorrectPapers = 3;  // Y: max incorrect papers allowed

    private int currentPaperIndex = 0;
    private int completedPapers = 0;
    private int incorrectPapers = 0;

    private bool canNext = false;
    private bool waitingForContinue = false;  // Flag to check if we're waiting for player to continue

    [Header("Clock")]
    public TimerClock timerClock;  // Reference to the TimerClock script
    [HideInInspector] public bool timerExpired = false;  // Flag for the timer expiration

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Disable all papers at the start
        foreach (GameObject paper in papers)
        {
            paper.SetActive(false);
        }

        // Find the TimerClock script in the scene
        timerClock = FindObjectOfType<TimerClock>();
    }

    public void StartSystem()
    {
        if (papers.Length > 0)
        {
            papers[0].SetActive(true);  // Enable the first paper
            completedPapers++;
            canNext = true;

            // Start the Timer System
            StartTimer();
        }
    }

    private void StartTimer()
    {
        timerClock.StartClock();
    }

    public void NextPaper(bool hoax)
    {
        if (!canNext) return;

        PaperProperties currentPaperProps = papers[currentPaperIndex].GetComponent<PaperProperties>();

        if (currentPaperProps.Hoax != hoax)
        {
            currentPaperProps.ErrorPaper.SetActive(true);
            incorrectPapers++;
            waitingForContinue = true;  // Set the flag to wait for player input
        }
        else
        {
            StartCoroutine(HandleNextPaper(currentPaperProps, hoax));
        }
    }

    public void ContinueAfterError()
    {
        if (incorrectPapers >= maxIncorrectPapers)
        {
            Outcome2();  // Trigger outcome 2
            return;
        }

        else if (waitingForContinue)
        {
            waitingForContinue = false;  // Reset the flag
            PaperProperties currentPaperProps = papers[currentPaperIndex].GetComponent<PaperProperties>();
            StartCoroutine(HandleNextPaper(currentPaperProps, currentPaperProps.Hoax));  // Continue as if the bools matched
        }
    }

    private IEnumerator HandleNextPaper(PaperProperties currentPaperProps, bool hoax)
    {
        canNext = false;
        yield return new WaitForSeconds(exitDelay);

        papers[currentPaperIndex].SetActive(false);  // Disable current paper

        if (timerExpired && completedPapers < papers.Length)
        {
            Outcome3();
        }

        else
        {
            if (currentPaperProps.Hoax == hoax)
            {
                currentPaperIndex++;  // Move to the next paper in the array

                if (currentPaperIndex < papers.Length)
                {
                    yield return new WaitForSeconds(nextPaperDelay);

                    papers[currentPaperIndex].SetActive(true);  // Enable the next paper
                    completedPapers++;
                    canNext = true;  // Set canNext to true only when a new paper is available
                }
                else
                {
                    Outcome1();  // Reached the end of the papers array, trigger outcome 1
                }
            }
        }
    }

    // Outcome 1: Called when the system reaches the end of the paper array
    private void Outcome1()
    {
        Debug.Log("Reached the end of the paper array.");
        
    }

    // Outcome 2: Called when incorrect papers exceed maxIncorrectPapers
    private void Outcome2()
    {
        Debug.Log("Too many incorrect papers.");
        
    }

    // Outcome 3: Called when the timer runs out and the player hasn't completed all papers
    private void Outcome3()
    {
        Debug.Log("Time's up! Player didn't complete all papers.");
      
    }
}
