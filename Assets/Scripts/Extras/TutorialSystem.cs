using UnityEngine;
using System.Collections;

public class TutorialSystem : MonoBehaviour
{
    public GameObject[] tutorialObjects;  // Array of tutorial objects
    public GameObject tutorialEffect;     // Effect shown during tutorial
    public float delayBetweenObjects = 1.0f;  // Delay before moving to the next object

    private int currentObjectIndex = 0;  // Track the current tutorial object
    private bool canProceed = false;     // Track if the player can proceed to the next tutorial object

    void Start()
    {
        // Disable all tutorial objects at the start
        foreach (GameObject obj in tutorialObjects)
        {
            obj.SetActive(false);
        }

        // Disable the tutorial effect at the start
        tutorialEffect.SetActive(false);
    }

    public void StartTutorial()
    {
        if (tutorialObjects.Length > 0)
        {
            tutorialObjects[0].SetActive(true);  // Enable the first tutorial object
            tutorialEffect.SetActive(true);      // Enable the tutorial effect
            currentObjectIndex = 0;
            canProceed = false;
            StartCoroutine(DelayNextStep());
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canProceed)
        {
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

        // Disable all tutorial objects
        foreach (GameObject obj in tutorialObjects)
        {
            obj.SetActive(false);
        }

        Debug.Log("Tutorial completed.");
        // You can add additional logic here for when the tutorial ends
    }
}
