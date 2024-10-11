using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectToggle : MonoBehaviour
{
    public InspectorManager inspectorManager;  // Reference to the InspectorSystem script
    public InspectorSystem inspectorSystem;
    public Animator animator;

    private bool canToggle = true;

    private void Start()
    {
        inspectorSystem.gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (inspectorManager != null && canToggle)
        {
            if (inspectorManager.isEnabled)
            {
                inspectorManager.DisableInspect();
                inspectorSystem.canCheck = false;
                inspectorSystem.gameObject.SetActive(false);
                StartCoroutine(bugDelay());
                animator.SetTrigger("Close");
            }
            else
            {
                inspectorManager.ToggleInspect();
                inspectorSystem.gameObject.SetActive(true);
                inspectorSystem.canCheck = true;
                StartCoroutine(bugDelay());
                animator.SetTrigger("Open");
            }
        }
    }

    private IEnumerator bugDelay()
    {
        canToggle = false;
        yield return new WaitForSeconds(1f);
        canToggle = true;
    }
}
