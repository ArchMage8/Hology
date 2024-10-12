using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseToggler : MonoBehaviour
{
    public Animator animator;
    public int AnimationWait;

    private bool isOpen = false;
    private bool canToggle = true;

    [Header("Audio")]
    public AudioClip ObjectIn;
    public AudioClip ObjectOut;
    public SFXManager SFXManager;

    private void OnMouseDown()
    {
        if (canToggle && !GameStateHandler.instance.isInspecting && !GameStateHandler.instance.isPrinting)
        {
            StartCoroutine(AnimationWaitToggle());
        }
    }

    private IEnumerator AnimationWaitToggle()
    {
        canToggle = false;
        if (isOpen == false)
        {
            Debug.Log("Open");
            animator.SetTrigger("Open");
            SFXManager.PlaySound(ObjectIn);
            yield return new WaitForSeconds(AnimationWait);
            canToggle = true;
            isOpen = true;
        }

        else if (isOpen == true)
        {
            Debug.Log("Close");
            animator.SetTrigger("Close");
            SFXManager.PlaySound(ObjectIn);
            yield return new WaitForSeconds(AnimationWait);
            canToggle = true;
            isOpen = false;

        }

    }

    public void ResponseExternalReference()
    {
          StartCoroutine(AnimationWaitToggle());
    }
}
