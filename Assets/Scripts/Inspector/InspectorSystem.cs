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

    [Header("Inspector Overlay")]
    public GameObject NewspaperHighlight;
    public GameObject BookHighlight;
    [HideInInspector]public GameObject ComponentHighlight;

    [Header("Settings")]
    public float displayDuration = 2f;  // Duration to display "No Connect", "PositiveImage", or "NegativeImage"

    [Header("Research System")]
    public GameObject GuideResearchPage;
    public GameObject IndependetPublisherPage;
    public GameObject ResearchIcon;
    public GameObject ResearchButton;

    [Header("Audio")]
    public AudioClip clickSound;
    public AudioClip CorrectSound;
    public AudioClip WrongSound;
    public AudioClip MismatchSound;

    public SFXManager_Exception MonitorSounds;


    private bool isInCheckMode = false;
    [HideInInspector] public bool canCheck = false;
    [HideInInspector] public bool AlreadyPrint = false;
    private InspectComponent currentInspectComponent;
    private GameObject Holder;
    private bool CanHighlight;

    //private bool isDisplaying;

    void Awake()
    {
        Instance = this;
    
        NewspaperHighlight.SetActive(false);
        instruction1.SetActive(true);
        instruction2.SetActive(false);
        noConnect.SetActive(false);
        positiveImage.SetActive(false);
        negativeImage.SetActive(false);
        ResearchIcon.SetActive(false);
        //ResearchButton.SetActive(false);
    }

    void Update()
    {
        if (!NPC_Main.Instance.Started || !PaperManager.Instance.PaperOnScreen)
        {
            NewspaperHighlight.SetActive(false);
        }


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
        SFXManager.instance.PlaySound(clickSound);
        isInCheckMode = true;
        instruction1.SetActive(false);
        instruction2.SetActive(true);

        NewspaperHighlight.SetActive(false);
       
        ComponentHighlight = currentInspectComponent.componentHighlight;
        ComponentHighlight.SetActive(true);
    }

    void HandleClickInCheckMode(GameObject clickedObject)
    {
        BookHighlight.SetActive(true);
        isInCheckMode = false;
        instruction2.SetActive(false);
        
       
 
        if (clickedObject.CompareTag("PaperComponent") || clickedObject.CompareTag("GuidePage"))
        {
            SFXManager.instance.PlaySound(clickSound);

            canCheck = false;
            //Research Excepetion for Feature Page
            if (clickedObject == GuideResearchPage && currentInspectComponent.Target_Page == GuideResearchPage)
            {
                StopCoroutine(DisplayTemporary(Holder));

                MonitorSounds.PlaySound(CorrectSound);
                StartCoroutine(DisplayTemporary(ResearchIcon));
                if (!AlreadyPrint)
                {
                    ResearchButton.SetActive(true);
                    AlreadyPrint = true;
                }
            }
            //Research Excepetion for Vague Pub Page
            else if (clickedObject == IndependetPublisherPage && currentInspectComponent.Target_Page == IndependetPublisherPage)
            {
                //Research Excepetion
                StopCoroutine(DisplayTemporary(Holder));

                MonitorSounds.PlaySound(CorrectSound);
                StartCoroutine(DisplayTemporary(ResearchIcon));
                if (!AlreadyPrint)
                {
                    ResearchButton.SetActive(true);
                    AlreadyPrint = true;
                }
            }

            //Normal
            else if (clickedObject == currentInspectComponent.Target_Page)
            {
                if (currentInspectComponent.positiveResponse)
                {
                    //StopCoroutine(DisplayTemporary(Holder));
                    MonitorSounds.PlaySound(CorrectSound);
                    StartCoroutine(DisplayTemporary(positiveImage));
                }
                else
                {
                    //StopCoroutine(DisplayTemporary(Holder));
                    MonitorSounds.PlaySound(WrongSound);
                    StartCoroutine(DisplayTemporary(negativeImage));
                }
            }
            else
            {
                //StopCoroutine(DisplayTemporary(Holder));
                MonitorSounds.PlaySound(MismatchSound);
                StartCoroutine(DisplayTemporary(noConnect));
            }
        }
    }

    IEnumerator DisplayTemporary(GameObject obj)
    {
        if (obj != null)
        {
            Debug.Log("vvv");

            obj.SetActive(true);
            yield return new WaitForSeconds(displayDuration);
            obj.SetActive(false);
            canCheck = true;
            instruction1.SetActive(true);

            BookHighlight.SetActive(false);
            ComponentHighlight.SetActive(false);
            //ComponentHighlight = null;
            NewspaperHighlight.SetActive(true);

        }
    }

    public void ResetDisplay()
    {
        //NewspaperHighlight.SetActive(false);

        if (ComponentHighlight != null) {
            ComponentHighlight.SetActive(false);
        }
       
        instruction1.SetActive(true);
        instruction2.SetActive(false);
        noConnect.SetActive(false);
        positiveImage.SetActive(false);
        negativeImage.SetActive(false);
        ResearchIcon.SetActive(false);
        //ResearchButton.SetActive(false);
    }
}
