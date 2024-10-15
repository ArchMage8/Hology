using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchToggle : MonoBehaviour
{
    public GameObject ResearchParent;
    public GameObject ResearchMonitor;
    public InspectToggle inspectToggle;

    [Header("Audio")]
    public AudioClip TrayIN;
    public SFXManager SFXManager;

    private void Awake()
    {
        ResearchParent.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (!GameStateHandler.instance.isPrinting)
        {
            ResearchMonitor.GetComponent<ResearchCode>().enabled = true;
            inspectToggle.ExternalResponse();
            ResearchParent.SetActive(true);
            
            this.gameObject.SetActive(false);

            SFXManager.instance.PlaySound(TrayIN);
        }
    }
}
