using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialToggle : MonoBehaviour
{
    public TutorialSystem tutorialSystem;

    private void OnMouseDown()
    {
        tutorialSystem.StartTutorial();
        this.gameObject.SetActive(false);
    }
}
