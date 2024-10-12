using UnityEngine;
using System.Collections;

public class PaperButtonHoax : MonoBehaviour
{
    public float AnimationDelay;
    private Animator animator;
    private Animator PaperAnimator;

    [Header("Audio")]
    public AudioClip ButtonPress;
    public SFXManager SFXManager;

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
        PaperAnimator = PaperManager.Instance.CurrentPaper.GetComponent<Animator>();
        PaperAnimator.SetTrigger("Out");
        SFXManager.PlaySound(ButtonPress);
        yield return new WaitForSeconds(AnimationDelay);
        PaperManager.Instance.NextPaper(true);
    }
}

