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

    public TextMeshProUGUI codeDisplayText;  // Reference to the TextMeshPro component for displaying the input

    [Header("Audio")]
    public AudioClip PrinterSound;
    public AudioClip TrayOut;
    public SFXManager_Exception SFXPrinter;
    public SFXManager SFXManager;

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

        GetCurrentPaper();
    }

    public void AddDigit(string digit)
    {
        if (playerInput.Length < 3)
        {
            playerInput += digit;
            UpdateDisplay();  // Update the displayed input
            if (playerInput.Length == 3)
            {
                CheckCode();
            }
        }
    }

    private IEnumerator CheckCode()
    {
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
        Debug.Log("Correct code entered!");
        currentResearchPaper.SetActive(true);
        SFXPrinter.PlaySound(PrinterSound);
        GameStateHandler.instance.isPrinting = true;
        StartCoroutine(DisableCodeMachine());
        
    }

    private void CodeWrong()
    {
        Debug.Log("Wrong code entered!");
        playerInput = "";
        // Add logic for wrong code
    }

    private void GetCurrentPaper()
    {
        GameObject activePaper = PaperManager.Instance.GetActivePaper();  // Get the active paper from PaperManager

        if (activePaper != null)
        {
            currentActivePaper = activePaper;
        }

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
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("Close");
        SFXManager.PlaySound(TrayOut);

        yield return new WaitForSeconds(1.5f);
        this.gameObject.SetActive(false);
    }
}
