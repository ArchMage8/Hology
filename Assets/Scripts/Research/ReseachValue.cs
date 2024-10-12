using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReseachValue : MonoBehaviour
{
    public string digit;

    public void OnMouseDown()
    {
        if (ResearchCode.Instance != null && !GameStateHandler.instance.isInspecting && !GameStateHandler.instance.isPrinting)
        {
            ResearchCode.Instance.AddDigit(digit);
        }
    }
}
