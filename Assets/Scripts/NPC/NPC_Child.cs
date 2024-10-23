using UnityEngine;
using System.Collections;

public class NPC_Child : MonoBehaviour
{
    public Transform Destination;
    public NPC_Main npcMain;
    public GameObject NPC_Dialogue;
    private bool isWalking = true;
    private bool hasInteracted = false;

    public float speed = 3f;
    public float dialogueDisplayTime = 2f;

    private void Start()
    {
        NPC_Dialogue.SetActive(true);
    }

    void Update()
    {
        if (isWalking)
        {
            WalkTowardsDestination();
        }
    }

    public void Walk()
    {
        isWalking = true;
    }

    private void WalkTowardsDestination()
    {
        if (Destination != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, Destination.position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, Destination.position) < 0.1f)
            {
                isWalking = false;
                npcMain.Walking = false;
            }
        }
    }
}
