using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InspectToggle : MonoBehaviour
{
    public InspectorManager inspectorManager;  // Reference to the InspectorSystem script
    public InspectorSystem inspectorSystem;
    public Animator animator;
    public ResponseToggler ResponseTray;
    public GameObject MaskingDim;

    [Header("Audio")]
    public AudioClip ObjectIn;
    public AudioClip ObjectOut;
    public SFXManager SFXManager;

    private bool canToggle = true;
    public bool IsVisible = false;

    private void Start()
    {
        inspectorSystem.gameObject.SetActive(false);
        MaskingDim.gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (inspectorManager != null && canToggle && !GameStateHandler.instance.isPrinting)
        {
            ResponseTray.ResponseExternal();



            if (inspectorManager.isEnabled)
            {
                IsVisible = false;
                MaskingDim.gameObject.SetActive(false);
                inspectorManager.DisableInspect();
                GameStateHandler.instance.isInspecting = false;
                inspectorSystem.canCheck = false;
                inspectorSystem.gameObject.SetActive(false);
                StartCoroutine(bugDelay());
                SFXManager.PlaySound(ObjectOut);
                animator.SetTrigger("Close");
                inspectorSystem.NewspaperHighlight.SetActive(false);
                DisableHighlights();
            }
            else
            {
                IsVisible = true;
                MaskingDim.gameObject.SetActive(true);
                inspectorSystem.ResetDisplay();
                inspectorManager.ToggleInspect();
                GameStateHandler.instance.isInspecting = true;
                inspectorSystem.gameObject.SetActive(true);
                inspectorSystem.canCheck = true;
                StartCoroutine(bugDelay());
                SFXManager.PlaySound(ObjectIn);
                animator.SetTrigger("Open");
                inspectorSystem.NewspaperHighlight.SetActive(true);
            }
        }
    }

    private IEnumerator bugDelay()
    {
        canToggle = false;
        yield return new WaitForSeconds(1f);
        canToggle = true;
    }

    public void ExternalResponse()
    {
        if (IsVisible)
        {
            inspectorManager.DisableInspect();
            MaskingDim.gameObject.SetActive(false);
            GameStateHandler.instance.isInspecting = false;
            inspectorSystem.canCheck = false;
            inspectorSystem.gameObject.SetActive(false);
            StartCoroutine(bugDelay());
            SFXManager.PlaySound(ObjectOut);
            animator.SetTrigger("Close");
            DisableHighlights();
        }
        else
        {
            return;
        }
    }

    private void DisableHighlights()
    {
        InspectorSystem.Instance.BookHighlight.SetActive(false);
        InspectorSystem.Instance.NewspaperHighlight.SetActive(false);
        if (InspectorSystem.Instance.ComponentHighlight != null)
        {
            InspectorSystem.Instance.ComponentHighlight.SetActive(false);
        }
        
    }
}
