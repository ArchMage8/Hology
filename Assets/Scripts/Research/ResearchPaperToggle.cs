using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchPaperToggle : MonoBehaviour
{
    private float AnimationWait = 1.5f;
    private Animator animator;
    private bool isVisible = true;

    [Header("Audio")]
    public AudioClip PaperIn;
    public AudioClip PaperOut;
    public SFXManager SFXManager;

    private void OnMouseDown()
    {
        if (!GameStateHandler.instance.isInspecting)
        {
            StartCoroutine(AnimationToggle());
        }
    }

    private IEnumerator AnimationToggle()
    {
        if (isVisible)
        {
            GameStateHandler.instance.isPrinting = false;
            animator.SetTrigger("Close");
            SFXManager.instance.PlaySound(PaperOut);
            yield return new WaitForSeconds(AnimationWait);
            this.gameObject.SetActive(false);
            
        }

        else
        {
            GameStateHandler.instance.isPrinting = true;
            animator.SetTrigger("Open");
            SFXManager.instance.PlaySound(PaperOut);
            yield return new WaitForSeconds(AnimationWait);
            this.gameObject.SetActive(false);
            
        }
    }
}
