using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchPaperDisable : MonoBehaviour
{
    private float AnimationWait = 1.5f;
    private Animator animator;

    private void OnMouseDown()
    {
        StartCoroutine(AnimationDisable());
    }

    private IEnumerator AnimationDisable()
    {
        animator.SetTrigger("Close");
        yield return new WaitForSeconds(AnimationWait);
        this.gameObject.SetActive(false);
    }
}
