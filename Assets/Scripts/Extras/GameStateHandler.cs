using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateHandler : MonoBehaviour
{
    public static GameStateHandler instance;

    [HideInInspector] public bool isPrinting = false;
    [HideInInspector] public bool isInspecting = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
