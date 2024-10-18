using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperErrorNext : MonoBehaviour
{

    public GameObject ParentObject;


    private void OnMouseDown()
    {
        Debug.Log(ParentObject.name);
        Animator animator = ParentObject.GetComponent<Animator>();
        animator.SetTrigger("Out");
        StartCoroutine(AnimationWait());
    }

    private IEnumerator AnimationWait()
    {
        yield return new WaitForSeconds(1f);
        GameStateHandler.instance.isPrinting = false;
        PaperManager.Instance.ContinueAfterError();
        ParentObject.SetActive(false);
    }
}
