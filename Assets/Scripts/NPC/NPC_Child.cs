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

    private void OnMouseDown()
    {
        if (!hasInteracted)
        {
            StopNPCAndShowDialogue();
        }
    }

    private void StopNPCAndShowDialogue()
    {
        isWalking = false;
        NPC_Dialogue.SetActive(true);
        StartCoroutine(ResumeWalkingAfterDialogue());
        hasInteracted = true;
    }

    private IEnumerator ResumeWalkingAfterDialogue()
    {
        yield return new WaitForSeconds(dialogueDisplayTime);
        NPC_Dialogue.SetActive(false);
        isWalking = true;
    }
}
