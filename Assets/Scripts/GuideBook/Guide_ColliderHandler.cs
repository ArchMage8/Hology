using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide_ColliderHandler : MonoBehaviour
{
    private PolygonCollider2D BookCollider;
    public bool exception = true;

    private void Start()
    {
        BookCollider = GetComponent<PolygonCollider2D>();
    }

    private void Update()
    {
        if (GameStateHandler.instance.isPrinting == true || GameStateHandler.instance.isInspecting == true || GameStateHandler.instance.isResearching == true)
        {
            BookCollider.enabled = false;
        }

        else
        {
            BookCollider.enabled = true;
        }
    }
}
