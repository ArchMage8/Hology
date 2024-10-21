using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperStarts : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip ButtonPress;
    public SFXManager SFXManager;

    private void OnMouseDown()
    {
        if (!GameStateHandler.instance.isInspecting)
        {
            PaperManager.Instance.StartSystem();
            //NPC_Main.Instance.StartSystem();
            SFXManager.PlaySound(ButtonPress);
            this.gameObject.SetActive(false);
        }
    }
}
