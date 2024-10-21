using UnityEngine;
using System.Collections;

public class NPC_Main : MonoBehaviour
{
    public static NPC_Main Instance { get; private set; }

    public GameObject[] NPCs;
    public bool Walking = false;
    public float delayBetweenNPCs = 30f;

    public bool Started = false;
    private int currentNPCIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        foreach (GameObject npc in NPCs)
        {
            npc.SetActive(false);
            
        }
        
    }

    public void StartSystem()
    {
        StartCoroutine(StartTheSystem());
    }

    private IEnumerator StartTheSystem()
    {

        yield return new WaitForSeconds(delayBetweenNPCs);
        ActivateNextNPC();
        Started = true;

    } 

    private void ActivateNextNPC()
    {
        Debug.Log("Test");
        if (currentNPCIndex < NPCs.Length)
        {
            NPCs[currentNPCIndex].SetActive(true);
            NPC_Child npcChild = NPCs[currentNPCIndex].GetComponent<NPC_Child>();
            npcChild.Walk();
           
        }
    }

    public IEnumerator WaitAndActivateNextNPC()
    {
        if (currentNPCIndex < NPCs.Length)
        {
            Walking = true;
            yield return new WaitForSeconds(delayBetweenNPCs);
            currentNPCIndex++;
            ActivateNextNPC();
        }
    }

    private void Update()
    {
        if (!Walking && Started)
        {
            StartCoroutine(WaitAndActivateNextNPC());
        }
    }
}
