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
        if (canToggle && GameStateHandler.instance.isInspecting == false && GameStateHandler.instance.isPrinting == false)
        {
            StartCoroutine(AnimationWaitToggle());
        }
    }

    private IEnumerator AnimationWaitToggle()
    {
        canToggle = false;
        if (isOpen == false)
        {
            isOpen = true;
            Debug.Log("Open");
            animator.SetTrigger("Open");
            SFXManager.PlaySound(ObjectIn);
            yield return new WaitForSeconds(AnimationWait);
            canToggle = true;
            
        }

        else if (isOpen == true)
        {
            isOpen = false;
            Debug.Log("Close");
            animator.SetTrigger("Close");
            SFXManager.PlaySound(ObjectIn);
            yield return new WaitForSeconds(AnimationWait);
            canToggle = true;
            

        }

    }

    public void ResponseExternal()
    {
        StartCoroutine(ResponseExternalReference());
    }

    private IEnumerator ResponseExternalReference()
    {
        if (isOpen == true)
        {
            Debug.Log("Close");
            animator.SetTrigger("Close");
            SFXManager.PlaySound(ObjectIn);
            yield return new WaitForSeconds(AnimationWait);
            canToggle = true;
            isOpen = false;

        }
    }
}
