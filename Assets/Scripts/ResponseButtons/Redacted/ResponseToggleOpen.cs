using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseToggleOpen : MonoBehaviour
{
    public GameObject ResponseTray;
    public float AnimationWait;

    private void Start()
    {
        PaperManager.Instance.canNext = false;
        ResponseTray.SetActive(false);
    }

    private void OnMouseDown()
    {
        StartCoroutine(AnimationWaitEnable());
    }


    private IEnumerator AnimationWaitEnable()
    {
       
        ResponseTray.SetActive(true);
        yield return new WaitForSeconds(AnimationWait);
        PaperManager.Instance.canNext = true;

    }
}
