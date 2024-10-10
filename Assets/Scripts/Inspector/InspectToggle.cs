using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectToggle : MonoBehaviour
{
    public InspectorManager inspectorManager;  // Reference to the InspectorSystem script
    public Animator animator;

    private void OnMouseDown()
    {
        if (inspectorManager != null)
        {
            if (inspectorManager.isEnabled)
            {
                inspectorManager.DisableInspect();
                animator.SetTrigger("Close");
            }
            else
            {
                inspectorManager.ToggleInspect();
                animator.SetTrigger("Open");
            }
        }
    }
}
