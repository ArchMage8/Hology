using UnityEngine;
using System.Collections;

public class InspectorSystem : MonoBehaviour
{
    [Header("Instructions")]
    public GameObject instruction1;
    public GameObject instruction2;

    [Header("Feedback Objects")]
    public GameObject noConnect;
    public GameObject positiveImage;
    public GameObject negativeImage;

    [Header("Settings")]
    public float displayDuration = 2f;  // Duration to display "No Connect", "PositiveImage", or "NegativeImage"

    private bool isInCheckMode = false;
    private bool canCheck;
    private InspectComponent currentInspectComponent;

    void Start()
    {
        instruction1.SetActive(true);
        instruction2.SetActive(false);
        noConnect.SetActive(false);
        positiveImage.SetActive(false);
        negativeImage.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canCheck == true)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D clickedCollider = Physics2D.OverlapPoint(mousePosition);

            if (clickedCollider != null)
            {
                if (!isInCheckMode)
                {
                    currentInspectComponent = clickedCollider.GetComponent<InspectComponent>();
                    if (currentInspectComponent != null)
                    {
                        EnterCheckMode();
                    }
                }
                else
                {
                    HandleClickInCheckMode(clickedCollider.gameObject);
                }
            }
        }
    }

    void EnterCheckMode()
    {
        isInCheckMode = true;
        instruction1.SetActive(false);
        instruction2.SetActive(true);
    }

    void HandleClickInCheckMode(GameObject clickedObject)
    {
        isInCheckMode = false;
        instruction2.SetActive(false);
        instruction1.SetActive(true);

        if (clickedObject.CompareTag("PaperComponent") || clickedObject.CompareTag("GuidePage"))
        {
            canCheck = false;
            if (clickedObject == currentInspectComponent.Target_Page)
            {
                if (currentInspectComponent.positiveResponse)
                {
                    StartCoroutine(DisplayTemporary(positiveImage));
                }
                else
                {
                    StartCoroutine(DisplayTemporary(negativeImage));
                }
            }
            else
            {
                StartCoroutine(DisplayTemporary(noConnect));
            }
        }
    }

    IEnumerator DisplayTemporary(GameObject obj)
    {
        obj.SetActive(true);
        yield return new WaitForSeconds(displayDuration);
        obj.SetActive(false);
        canCheck = true;
    }
}
