using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectToggle : MonoBehaviour
{
    public InspectorManager inspectorManager;  // Reference to the InspectorSystem script

    private void OnMouseDown()
    {
        if (inspectorManager != null)
        {
            if (inspectorManager.isEnabled)
            {
                inspectorManager.DisableInspect();
            }
            else
            {
                inspectorManager.ToggleInspect();
            }
        }
    }
}
