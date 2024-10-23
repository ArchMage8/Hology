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

    private bool HasBeenInteracted = false;

    private void OnEnable()
    {
       StartCoroutine(StartLogic());
    }

    public void StartTutorial()
    {
        if (!Started)
        {
            


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
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canProceed && Started)
        {
            //Debug.Log("Test");
            ProceedToNextObject();
        }
    }

    private void ProceedToNextObject()
    {
       
            tutorialObjects[currentObjectIndex].SetActive(false);  // Disable the current object
            
        

        if (currentObjectIndex < tutorialObjects.Length - 1 && canProceed)
        {
            currentObjectIndex++;  // Move to the next object
            tutorialObjects[currentObjectIndex].SetActive(true);  // Enable the next object
            canProceed = false;
            StartCoroutine(DelayNextStep());
        }
        else if(currentObjectIndex == tutorialObjects.Length - 1)
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
        Debug.Log("BBB");

        Started = false;
        tutorialEffect.SetActive(false);  // Disable the tutorial effect
        
        ToggleButton.GetComponent<BoxCollider2D>().enabled = true;
        GameStateHandler.instance.isResearching = false;
        GameStateHandler.instance.isPrinting = false;
        GameStateHandler.instance.isInspecting = false;

        // Disable all tutorial objects
        foreach (GameObject obj in tutorialObjects)
        {
            obj.SetActive(false);
        }

        currentObjectIndex = 0;

        //this.enabled = false;
        //Debug.Log("Tutorial completed.");
        //this.GetComponent<TutorialSystem>().enabled = false;
        // You can add additional logic here for when the tutorial ends
    }

    private IEnumerator StartLogic()
    {
        yield return new WaitForSeconds(0.2f);

        if (!OnAtStart)
        {
            HasBeenInteracted = true;

            // Disable all tutorial objects at the start
            foreach (GameObject obj in tutorialObjects)
            {
                obj.SetActive(false);
            }

            // Disable the tutorial effect at the start
            tutorialEffect.SetActive(false);
        }

        else if (!HasBeenInteracted)
        {
            HasBeenInteracted = true;
            currentObjectIndex = 0;
            StartTutorial();
        }
    }
}
