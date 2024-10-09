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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits the object with this script attached
            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                StartCoroutine(AnimationWaitEnable());
            }
        }
    }

    private IEnumerator AnimationWaitEnable()
    {
        ResponseTray.SetActive(true);
        yield return new WaitForSeconds(AnimationWait);
        PaperManager.Instance.canNext = true;

    }
}
