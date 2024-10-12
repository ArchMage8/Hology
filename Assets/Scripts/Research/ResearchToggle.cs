using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchToggle : MonoBehaviour
{
    public GameObject ResearchSystem;

    [Header("Audio")]
    public AudioClip TrayIN;
    public SFXManager SFXManager;

    private void Awake()
    {
        ResearchSystem.SetActive(false);
    }

    private void OnMouseDown()
    {
        ResearchSystem.SetActive(true);
        this.gameObject.SetActive(false);

        SFXManager.instance.PlaySound(TrayIN);
    }
}
