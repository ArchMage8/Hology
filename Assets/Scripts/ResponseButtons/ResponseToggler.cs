using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseToggler : MonoBehaviour
{
    public Animator animator;
    public int AnimationWait;

    private bool isOpen = false;
    private bool canToggle = true;

    private void OnMouseDown()
    {
        if (canToggle)
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
            yield return new WaitForSeconds(AnimationWait);
            canToggle = true;
            isOpen = true;
        }

        else if (isOpen == true)
        {
            Debug.Log("Close");
            animator.SetTrigger("Close");
            yield return new WaitForSeconds(AnimationWait);
            canToggle = true;
            isOpen = false;

        }

    }
}
