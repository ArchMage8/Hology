using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseToggleClose : MonoBehaviour
{
    public GameObject TogglerForWhenClosed;
    public GameObject ResponseTray;
    public float AnimationWait;


    private void OnMouseDown()
    {
        StartCoroutine(AnimationWaitEnable());
    }


    private IEnumerator AnimationWaitEnable()
    {
        
        Animator animator = ResponseTray.GetComponent<Animator>();
        animator.SetTrigger("Close");
        yield return new WaitForSeconds(AnimationWait);
        TogglerForWhenClosed.SetActive(true);
    }
}
