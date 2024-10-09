using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseToggle : MonoBehaviour
{
    public GameObject Buttons;
    public float AnimationWait;
    private Animator animator;

    private void Start()
    {
        animator = Buttons.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Detect left mouse click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits the object with this script attached
            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                ToggleGameObject();  // Call the function to toggle the target GameObject
            }
        }
    }

    private void ToggleGameObject()
    {
        if (Buttons != null)
        {
            bool isActive = Buttons.activeSelf;

            if (isActive)
            {
                StartCoroutine(AnimationWaitEnable());
            }

            else if (!isActive)
            {
                StartCoroutine(AnimationWaitDisable());
            }
        }
    }

    private IEnumerator AnimationWaitEnable()
    {
        Buttons.SetActive(true);
        yield return new WaitForSeconds(0f);
        
    }
    
    private IEnumerator AnimationWaitDisable()
    {
        animator.SetTrigger("Close");
        yield return new WaitForSeconds(AnimationWait);
        Buttons.SetActive(false);
    }
}
