using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchToggle : MonoBehaviour
{
    public GameObject ResearchSystem;

    private void Awake()
    {
        ResearchSystem.SetActive(false);
    }

    private void OnMouseDown()
    {
        ResearchSystem.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
