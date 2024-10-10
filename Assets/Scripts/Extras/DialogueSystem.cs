using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueSystem : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;  // Reference to the TextMeshPro object
    public string[] dialogues;  // Array of dialogues
    public float typingSpeed = 0.05f;  // Speed at which text appears
    public float NextDelay = 1.0f;  // Delay before moving to the next dialogue

    private int currentDialogueIndex = 0;  // Track current dialogue
    private bool isTyping = false;  // Track if typing is in progress
    private bool canProceed = false;  // Track if player can proceed to next dialogue
    private Coroutine typingCoroutine;

    void Start()
    {
        if (dialogues.Length > 0)
        {
            StartDialogue();  // Start the first dialogue
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                // If typing is ongoing, finish the current dialogue instantly
                StopTypingAndShowFullDialogue();
            }
            else if (!isTyping && canProceed)
            {
               ProceedToNextDialogue();
            }
        }
    }

    public void StartDialogue()
    {
        // Start displaying the first dialogue with typing effect
        typingCoroutine = StartCoroutine(TypeDialogue(dialogues[currentDialogueIndex]));
    }

    private IEnumerator TypeDialogue(string dialogue)
    {
        isTyping = true;
        canProceed = false;
        dialogueText.text = "";  // Clear the text before typing starts

        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);  // Wait before showing the next letter
        }

        isTyping = false;
        canProceed = true;
        StartCoroutine(DialogueDelay());
    }

    private void StopTypingAndShowFullDialogue()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);  // Stop the typing coroutine
        }

        // Show the entire current dialogue instantly
        dialogueText.text = dialogues[currentDialogueIndex];
        isTyping = false;
        StartCoroutine(DialogueDelay());
    }

    private IEnumerator DialogueDelay()
    {
        canProceed = false;
        yield return new WaitForSeconds(NextDelay);
        canProceed = true;
    }

    private void ProceedToNextDialogue()
    {
        currentDialogueIndex++;

        if (currentDialogueIndex < dialogues.Length)
        {
            // Start typing the next dialogue
            typingCoroutine = StartCoroutine(TypeDialogue(dialogues[currentDialogueIndex]));
        }
        else
        {
            // End of dialogue array, call a function (add logic here later)
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        Debug.Log("End of dialogue array.");
        // Logic to be added later
    }
}
