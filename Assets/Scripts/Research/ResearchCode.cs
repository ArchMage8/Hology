using UnityEngine;
using TMPro;
using System.Collections;

public class ResearchCode : MonoBehaviour
{
    public static ResearchCode Instance;  // Singleton instance
    private string correctCode;    // The correct answer code
    private string playerInput = "";      // Stores the player's input

    public PaperManager paperManager;
    private GameObject currentActivePaper;
    private GameObject currentResearchPaper;
    private bool isChecking = false;
    private bool canEnter = true;

    public TextMeshProUGUI codeDisplayText;  // Reference to the TextMeshPro component for displaying the input

    [Header("Audio")]
    public AudioClip PrinterSound;
    public AudioClip TrayOut;
    public SFXManager_Exception SFXPrinter;
    public SFXManager SFXManager;

    [Header("Main Machine")]
    public Animator MachineAnimator;
    public GameObject ERR_Text;
    public GameObject ACC_Text;

    void Awake()
    {
        // Ensure that there is only one instance of ResearchCode
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        ERR_Text.SetActive(false);
        ACC_Text.SetActive(false);
    }

    public void AddDigit(string digit)
    {
        if (canEnter)
        {
            if (playerInput.Length < 3)
            {
                playerInput += digit;
                UpdateDisplay();  // Update the displayed input
                if (playerInput.Length == 3)
                {
                    StartCoroutine(CheckCode());
                }
            }
        }
    }

    private IEnumerator CheckCode()
    {
        GetCurrentPaper();
        isChecking = true;
        
        yield return new WaitForSeconds(1.5f);

        if (playerInput == correctCode)
        {
            CodeCorrect();
        }
        else
        {
            CodeWrong();
        }

        playerInput = "";  // Reset the input
        UpdateDisplay();   // Clear the display
    }

    private void CodeCorrect()
    {
        
        currentResearchPaper.SetActive(true);
        SFXPrinter.PlaySound(PrinterSound);
        GameStateHandler.instance.isResearching = false;
        GameStateHandler.instance.isPrinting = true;
        MachineAnimator.SetTrigger("Out");
        
        isChecking = false;
        StartCoroutine(DisableCodeMachine());

    }

    private void CodeWrong()
    {
        //Debug.Log("Wrong code entered!");
        playerInput = "";
        isChecking = false;
        // Add logic for wrong code
    }

    private void GetCurrentPaper()
    {
        currentActivePaper = PaperManager.Instance.GetActivePaper();
        GameObject activePaper = currentActivePaper;

        PaperProperties currentProperties = activePaper.GetComponent<PaperProperties>();
        correctCode = currentProperties.ResearchCode;
        currentResearchPaper = currentProperties.ResearchPaper;
    }

    // Update the TextMeshPro component to show the current input
    private void UpdateDisplay()
    {
        if (codeDisplayText != null)
        {
            codeDisplayText.text = playerInput;  // Update the text with the current input
        }
    }

    private IEnumerator DisableCodeMachine()
    {
        //Animator animator = GetComponent<Animator>();
        //animator.SetTrigger("Close");
        SFXManager.PlaySound(TrayOut);
        //Debug.Log("Test");
        yield return new WaitForSeconds(1.5f);
        this.gameObject.SetActive(false);
        MachineAnimator.gameObject.SetActive(false);
    }

    private IEnumerator CorrectCode()
    {
        ACC_Text.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        ACC_Text.SetActive(false);
        CodeCorrect();
    }

    private IEnumerator IncorrectCode()
    {
        canEnter = false;
        ERR_Text.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        ERR_Text.SetActive(false);
        canEnter = true;
    }
}
