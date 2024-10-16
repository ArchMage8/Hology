using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide_ColliderHandler : MonoBehaviour
{
    private PolygonCollider2D BookCollider;

    private void Start()
    {
        BookCollider = GetComponent<PolygonCollider2D>();    
    }

    private void Update()
    {
        if (GameStateHandler.instance.isPrinting || GameStateHandler.instance.isInspecting || GameStateHandler.instance.isResearching)
        {
            BookCollider.enabled = false;
        }

        else
        {
            BookCollider.enabled = true;
        }
    }
}
