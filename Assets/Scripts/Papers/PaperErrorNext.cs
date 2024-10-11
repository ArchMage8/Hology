using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperErrorNext : MonoBehaviour
{

    public GameObject ParentObject;


    private void OnMouseDown()
    {
        Animator animator = ParentObject.GetComponent<Animator>();
        animator.SetTrigger("Out");
        StartCoroutine(AnimationWait());
    }

    private IEnumerator AnimationWait()
    {
        yield return new WaitForSeconds(1f);
        PaperManager.Instance.ContinueAfterError();
        ParentObject.SetActive(false);
    }
}
