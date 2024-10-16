using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerBlink : MonoBehaviour
{
    public bool blink = false;
    private TextMeshProUGUI colonText;

    private void Update()
    {
        if (blink)
        {
            StartCoroutine(blinking());
        }
    }

    private IEnumerator blinking()
    {
        while (true)
        {
            colonText.enabled = false;
            yield return new WaitForSeconds(3f);
            colonText.enabled = true;
            yield return new WaitForSeconds(3f);
        }
    }
}
