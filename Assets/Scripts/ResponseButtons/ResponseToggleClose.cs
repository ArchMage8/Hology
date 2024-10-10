using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseToggleClose : MonoBehaviour
{
    public GameObject TogglerForWhenClosed;
    public GameObject ResponseTray;
    public float AnimationWait;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits the object with this script attached
            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                StartCoroutine(AnimationWaitEnable());
            }
        }
    }

    private IEnumerator AnimationWaitEnable()
    {
        Animator animator = ResponseTray.GetComponent<Animator>();
        animator.SetTrigger("Close");
        yield return new WaitForSeconds(AnimationWait);
        TogglerForWhenClosed.SetActive(true);
    }
}
