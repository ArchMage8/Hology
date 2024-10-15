using UnityEngine;
using System.Collections;
using TMPro;

public class PaperManager : MonoBehaviour
{
    public static PaperManager Instance { get; private set; }

    [Header("Paper Objects")]
    public GameObject[] papers;  // Array of gameobjects with PaperProperties
    [HideInInspector] public GameObject CurrentPaper;

    [Header("Delay Settings")]
    public float exitDelay = 2.0f;  // Delay before current paper is disabled
    public float nextPaperDelay = 1.0f;  // Delay before next paper is enabled

    [Header("Outcome Settings")]
    public int maxIncorrectPapers = 3;  // Y: max incorrect papers allowed
    [HideInInspector] public bool EndOfArray = false;

    private int currentPaperIndex = 0;
    private int completedPapers = 0;
    private int incorrectPapers = 0;

    public bool canNext = false;
    private bool waitingForContinue = false;  // Flag to check if we're waiting for player to continue

    [Header("Clock")]
    public TimerClock timerClock;  // Reference to the TimerClock script
    [HideInInspector] public bool timerExpired = false;  // Flag for the timer expiration
    [HideInInspector] public bool Started = false;

    [Header("Audio")]
    public AudioClip PaperIN;
    public AudioClip PaperOUT;
    public SFXManager SFXManager;
    [Space(10)]
    public AudioClip PrinterSound;
    public SFXManager_Exception PrinterSoundPlayer;



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
            SFXManager.PlaySound(PaperIN);
            completedPapers++;
            canNext = true;
            Started = true;

            // Start the Timer System
            StartTimer();

            CurrentPaper = papers[0];
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
        canNext = false;

        if (currentPaperProps.Hoax != hoax)
        {
            StartCoroutine(printError(currentPaperProps));
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
        SFXManager.PlaySound(PaperOUT);
        //canNext = false;
        yield return new WaitForSeconds(exitDelay);

        StartCoroutine(DisableCurrentPaper());

        if (timerExpired && completedPapers < papers.Length)
        {
            Outcome3();
        }

        else
        {
            if (currentPaperProps.Hoax == hoax)
            {
                StartCoroutine(DeletingResearchPaper());
                currentPaperIndex++;  // Move to the next paper in the array

                if (currentPaperIndex < papers.Length)
                {
                    yield return new WaitForSeconds(nextPaperDelay);

                    SFXManager.PlaySound(PaperIN);
                    papers[currentPaperIndex].SetActive(true);  // Enable the next paper
                    completedPapers++;
                    CurrentPaper = papers[currentPaperIndex];
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
        InGameSceneNavigator.Instance.FinishLevel();
    }

    // Outcome 2: Called when incorrect papers exceed maxIncorrectPapers
    private void Outcome2()
    {
        InGameSceneNavigator.Instance.ExceedMistakeLimit();
    }

    // Outcome 3: Called when the timer runs out and the player hasn't completed all papers
    private void Outcome3()
    {
        InGameSceneNavigator.Instance.ExceedTimeLimit();
    }

    public GameObject GetActivePaper()
    {
        if (currentPaperIndex < papers.Length && papers[currentPaperIndex].activeSelf)
        {
            return papers[currentPaperIndex];  // Return the active paper
        }
        return null;  // If no paper is active, return null
    }

    private IEnumerator DeletingResearchPaper()
    {
        GameStateHandler.instance.isPrinting = false;
        GameObject Temp = GetActivePaper();

        GameObject CurrResearchPaper = Temp.GetComponent<PaperProperties>().ResearchPaper;

        Animator animator = CurrResearchPaper.GetComponent<Animator>();

        animator.SetTrigger("Close");
        SFXManager.PlaySound(PaperOUT);
        yield return new WaitForSeconds(1.5f);
        CurrResearchPaper.SetActive(false);
    }

    private IEnumerator DisableCurrentPaper()
    {
        
        GameObject Temp = GetActivePaper();

        Animator animator = Temp.GetComponent<Animator>();

        animator.SetTrigger("Close");

        yield return new WaitForSeconds(1.5f);
        Temp.SetActive(false);  // Disable current paper
    }

    private IEnumerator printError(PaperProperties currProps)
    {
        DisableCurrentPaper();
        yield return new WaitForSeconds(1.5f);
        currProps.ErrorPaper.SetActive(true);
        incorrectPapers++;
        PrinterSoundPlayer.PlaySound(PrinterSound);
        waitingForContinue = true;  // Set the flag to wait for player input
    }
}
