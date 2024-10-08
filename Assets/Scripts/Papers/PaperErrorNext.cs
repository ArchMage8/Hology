using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperErrorNext : MonoBehaviour
{

    public GameObject ParentObject;

    private void OnMouseDown()
    {
        PaperManager.Instance.ContinueAfterError();
        ParentObject.SetActive(false);
    }
}
