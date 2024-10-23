using UnityEngine;
using System.Collections;

public class PaperButtonCorrect : MonoBehaviour
{
    public float AnimationDelay;
    private Animator animator;
    public Animator ResearchMachineAnimator;
    private Animator PaperAnimator;
    private bool CanPress = true;

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
        if (CanPress)
        {
            CanPress = false;
            animator.SetTrigger("Press");
            SFXManager.PlaySound(ButtonPress);

            if (PaperManager.Instance.Started == true)
            {
                PaperAnimator = PaperManager.Instance.GetActivePaper().GetComponent<Animator>();
                PaperAnimator.SetTrigger("Close");
                
                //SFXManager.PlaySound(ButtonPress);
                yield return new WaitForSeconds(AnimationDelay);
                CanPress = true;
                PaperManager.Instance.NextPaper(false);
            }
            else
            {
                yield return new WaitForSeconds(AnimationDelay);
                CanPress = true;
                yield return null;
            }
        }
    }

    private IEnumerator DisableResearchMachine()
    {
        if (ResearchMachineAnimator.gameObject.activeSelf)
        {
            ResearchMachineAnimator.SetTrigger("Out");
            yield return new WaitForSeconds(AnimationDelay);
            ResearchMachineAnimator.gameObject.SetActive(false);
        }
    }
}

