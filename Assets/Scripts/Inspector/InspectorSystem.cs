using UnityEngine;
using System.Collections;

public class InspectorSystem : MonoBehaviour
{
    public static InspectorSystem Instance { get; private set; }

    [Header("Instructions")]
    public GameObject instruction1;
    public GameObject instruction2;

    [Header("Feedback Objects")]
    public GameObject noConnect;
    public GameObject positiveImage;
    public GameObject negativeImage;

    [Header("Settings")]
    public float displayDuration = 2f;  // Duration to display "No Connect", "PositiveImage", or "NegativeImage"

    [Header("Research System")]
    public GameObject GuideResearchPage;
    public GameObject IndependetPublisherPage;
    public GameObject ResearchIcon;
    public GameObject ResearchButton;


    private bool isInCheckMode = false;
    [HideInInspector] public bool canCheck = false;
    [HideInInspector] public bool AlreadyPrint = false;
    private InspectComponent currentInspectComponent;

    void Awake()
    {
        Instance = this;

        instruction1.SetActive(true);
        instruction2.SetActive(false);
        noConnect.SetActive(false);
        positiveImage.SetActive(false);
        negativeImage.SetActive(false);
        ResearchIcon.SetActive(false);
        ResearchButton.SetActive(false);
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
        

 
        if (clickedObject.CompareTag("PaperComponent") || clickedObject.CompareTag("GuidePage"))
        {
            canCheck = false;
            if(clickedObject == GuideResearchPage && currentInspectComponent.Target_Page == GuideResearchPage)
            {
                //Research Excepetion
                StartCoroutine(DisplayTemporary(ResearchIcon));
                if (!AlreadyPrint)
                {
                    ResearchButton.SetActive(true);
                    AlreadyPrint = true;
                }
            }

            else if (clickedObject == IndependetPublisherPage && currentInspectComponent.Target_Page == IndependetPublisherPage)
            {
                //Research Excepetion
                StartCoroutine(DisplayTemporary(ResearchIcon));
                if (!AlreadyPrint)
                {
                    ResearchButton.SetActive(true);
                    AlreadyPrint = true;
                }
            }


            else if (clickedObject == currentInspectComponent.Target_Page)
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
        instruction1.SetActive(true);
    }

    public void ResetDisplay()
    {
        instruction1.SetActive(true);
        instruction2.SetActive(false);
        noConnect.SetActive(false);
        positiveImage.SetActive(false);
        negativeImage.SetActive(false);
        ResearchIcon.SetActive(false);
        ResearchButton.SetActive(false);
    }
}
