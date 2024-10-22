using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableToy_ID : MonoBehaviour
{
    private Animator animator;
    private bool canPlay = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        if (canPlay == true)
        {
            canPlay = false;
            animator.SetTrigger("Swing");
            
        }

    }

    public void EnablePlay()
    {
        Debug.Log("pLAY");
        canPlay = true;
    }

}