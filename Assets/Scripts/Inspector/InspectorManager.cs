using UnityEngine;

public class InspectorManager : MonoBehaviour
{

    [Header("GameObject Arrays")]
    public GameObject[] bookmarks;  
    public GameObject[] navigationButtons;
    public GameObject[] paperColliders;

    [Header("GameObject References")]
    public GameObject InspectEffect;
    public BoxCollider2D guideMainCollider;
    [Space(20)]

    public GameObject InspectHandler;

    [HideInInspector] public bool isEnabled = false;

    private void Start()
    {
        BackGroundSystem_Disable();
    }

    public void ToggleInspect()
    {
        BackgroundSystem_Toggle();
        InspectHandler.SetActive(true);
    }


    public void DisableInspect()
    {
        BackGroundSystem_Disable();
        InspectHandler.SetActive(true);
    }

    private void BackgroundSystem_Toggle()
    {
        isEnabled = !isEnabled;

        InspectEffect.SetActive(true);

        foreach (var button in navigationButtons)
        {
            if (button != null)
            {
                button.SetActive(!isEnabled);
            }
        }

        foreach (var Colliders in paperColliders)
        {
            if(Colliders != null)
            {
                Colliders.SetActive(true);
            }
        }

        foreach (var bookmark in bookmarks)
        {
            if (bookmark != null)
            {
                Collider2D collider = bookmark.GetComponent<Collider2D>();
                if (collider != null)
                {
                    collider.enabled = !isEnabled;
                }
            }
        }

        if (guideMainCollider != null)
        {
            guideMainCollider.enabled = !isEnabled;
        }
    }

    private void BackGroundSystem_Disable()
    {
        isEnabled = false;

        InspectEffect.SetActive(false);

        foreach (var button in navigationButtons)
        {
            if (button != null)
            {
                button.SetActive(true);
            }
        }

        foreach (var bookmark in bookmarks)
        {
            if (bookmark != null)
            {
                Collider2D collider = bookmark.GetComponent<Collider2D>();
                if (collider != null)
                {
                    collider.enabled = true;
                }
            }
        }

        foreach (var Colliders in paperColliders)
        {
            if (Colliders != null)
            {
                Colliders.SetActive(false);
            }
        }

        if (guideMainCollider != null)
        {
            guideMainCollider.enabled = true;
        }
    }
}

