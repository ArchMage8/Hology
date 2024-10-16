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
        if (PaperManager.Instance.canNext && !GameStateHandler.instance.isPrinting)
        {
            StartCoroutine(AnimationWait());
        }
    }

    private IEnumerator AnimationWait()
    {
        animator.SetTrigger("Press");
        if (PaperManager.Instance.Started)
        {
            PaperAnimator = PaperManager.Instance.GetActivePaper().GetComponent<Animator>();
            PaperAnimator.SetTrigger("Close");
            SFXManager.PlaySound(ButtonPress);
            yield return new WaitForSeconds(AnimationDelay);
            PaperManager.Instance.NextPaper(true);
        }
        else
        {
            yield return null;
        }
    }
}

