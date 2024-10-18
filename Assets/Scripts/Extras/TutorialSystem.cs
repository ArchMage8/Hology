using UnityEngine;
using System.Collections;

public class TutorialSystem : MonoBehaviour
{
    public GameObject[] tutorialObjects;  // Array of tutorial objects
    public GameObject tutorialEffect;     // Effect shown during tutorial
    public GameObject ToggleButton;
    public float delayBetweenObjects = 1.0f;  // Delay before moving to the next object
    public bool OnAtStart = true;

    private int currentObjectIndex = 0;  // Track the current tutorial object
    private bool canProceed = false;     // Track if the player can proceed to the next tutorial object
    private bool Started = false;


    private void OnEnable()
    {
       StartCoroutine(StartLogic());
    }

    public void StartTutorial()
    {

        ToggleButton.SetActive(false);

        
        GameStateHandler.instance.isPrinting = true;
        GameStateHandler.instance.isInspecting = true;
        GameStateHandler.instance.isResearching = true;

        if (tutorialObjects.Length > 0)
        {

            tutorialObjects[0].SetActive(true);  // Enable the first tutorial object
            tutorialEffect.SetActive(true);      // Enable the tutorial effect
            currentObjectIndex = 0;
            canProceed = false;
            StartCoroutine(DelayNextStep());
            Started = true;
        }
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canProceed && Started)
        {
            Debug.Log("Test");
            ProceedToNextObject();
        }
    }

    private void ProceedToNextObject()
    {
       
            tutorialObjects[currentObjectIndex].SetActive(false);  // Disable the current object
            currentObjectIndex++;  // Move to the next object
        

        if (currentObjectIndex < tutorialObjects.Length)
        {
            tutorialObjects[currentObjectIndex].SetActive(true);  // Enable the next object
            canProceed = false;
            StartCoroutine(DelayNextStep());
        }
        else
        {
            EndTutorial();  // End the tutorial when all objects have been shown
        }
    }

    private IEnumerator DelayNextStep()
    {
        yield return new WaitForSeconds(delayBetweenObjects);
        canProceed = true;  // Allow player to click and proceed to the next object
    }

    private void EndTutorial()
    {
        tutorialEffect.SetActive(false);  // Disable the tutorial effect
        ToggleButton.SetActive(true);

        GameStateHandler.instance.isResearching = false;
        GameStateHandler.instance.isPrinting = false;
        GameStateHandler.instance.isInspecting = false;

        // Disable all tutorial objects
        foreach (GameObject obj in tutorialObjects)
        {
            obj.SetActive(false);
        }

        Debug.Log("Tutorial completed.");
        // You can add additional logic here for when the tutorial ends
    }

    private IEnumerator StartLogic()
    {
        yield return new WaitForSeconds(0.2f);

        if (!OnAtStart)
        {
            // Disable all tutorial objects at the start
            foreach (GameObject obj in tutorialObjects)
            {
                obj.SetActive(false);
            }

            // Disable the tutorial effect at the start
            tutorialEffect.SetActive(false);
        }

        else
        {
            StartTutorial();
        }
    }
}
