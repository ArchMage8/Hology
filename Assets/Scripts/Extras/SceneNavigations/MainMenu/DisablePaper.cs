using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePaper : MonoBehaviour
{
    private Animator animator;
    public AudioClip paperOut;

    private void OnMouseDown()
    {
        
        this.gameObject.SetActive(false);
    }

    private IEnumerator AnimationDelay()
    {
        animator.SetTrigger("Out");
        SFXManager.instance.PlaySound(paperOut);
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
        MainMenuSystem.Instance.PaperActive = false;
    }
}

