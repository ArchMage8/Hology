using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperStarts : MonoBehaviour
{
    private void OnMouseDown()
    {
        PaperManager.Instance.StartSystem();
    }
}
