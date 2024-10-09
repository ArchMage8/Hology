using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReseachValue : MonoBehaviour
{
    public string digit;

    public void OnMouseDown()
    {
        if (ResearchCode.Instance != null)
        {
            ResearchCode.Instance.AddDigit(digit);
        }
    }
}
