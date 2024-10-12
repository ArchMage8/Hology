using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectToggle : MonoBehaviour
{
    public InspectorManager inspectorManager;  // Reference to the InspectorSystem script
    public InspectorSystem inspectorSystem;
    public Animator animator;
    public ResponseToggler ResponseTray;

    [Header("Audio")]
    public AudioClip ObjectIn;
    public AudioClip ObjectOut;
    public SFXManager SFXManager;

    private bool canToggle = true;

    private void Start()
    {
        inspectorSystem.gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (inspectorManager != null && canToggle && !GameStateHandler.instance.isPrinting)
        {
            ResponseTray.ResponseExternal();



            if (inspectorManager.isEnabled)
            {
                inspectorManager.DisableInspect();
                GameStateHandler.instance.isInspecting = false;
                inspectorSystem.canCheck = false;
                inspectorSystem.gameObject.SetActive(false);
                StartCoroutine(bugDelay());
                SFXManager.PlaySound(ObjectOut);
                animator.SetTrigger("Close");
            }
            else
            {
                inspectorManager.ToggleInspect();
                GameStateHandler.instance.isInspecting = true;
                inspectorSystem.gameObject.SetActive(true);
                inspectorSystem.canCheck = true;
                StartCoroutine(bugDelay());
                SFXManager.PlaySound(ObjectIn);
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
