using UnityEngine;
using System.Collections;

public class PaperButtonCorrect : MonoBehaviour
{
    public float AnimationDelay;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        if (PaperManager.Instance.canNext)
        {
            StartCoroutine(AnimationWait());
        }
    }

        private IEnumerator AnimationWait() 
    {
        animator.SetTrigger("Press");
        yield return new WaitForSeconds(AnimationDelay);
        PaperManager.Instance.NextPaper(false);
    }
}

