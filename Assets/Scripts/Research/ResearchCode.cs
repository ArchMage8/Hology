using UnityEngine;
using TMPro;

public class ResearchCode : MonoBehaviour
{
    public static ResearchCode Instance;  // Singleton instance
    private string correctCode;    // The correct answer code
    private string playerInput = "";      // Stores the player's input

    public PaperManager paperManager;
    private GameObject currentActivePaper;
    private GameObject currentResearchPaper;

    public TextMeshProUGUI codeDisplayText;  // Reference to the TextMeshPro component for displaying the input

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

    private void CheckCode()
    {
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
        this.gameObject.SetActive(false);
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
}
