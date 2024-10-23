using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialToggle : MonoBehaviour
{
    public TutorialSystem tutorialSystem;

    private void OnMouseDown()
    {
        if (!GameStateHandler.instance.isInspecting && !GameStateHandler.instance.isResearching && !GameStateHandler.instance.isPrinting)
        {
            tutorialSystem.enabled = true;
            tutorialSystem.StartTutorial();
            
            BoxCollider2D temp = gameObject.GetComponent<BoxCollider2D>();
            temp.enabled = false;
        }
    }
}
