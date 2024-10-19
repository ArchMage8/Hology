using UnityEngine;

public class Guide_Next : MonoBehaviour
{
    public Guide_Main guideMain;
    [Header("Audio")]
    public AudioClip PageSound;
    void OnMouseDown()
    {
        if (guideMain != null)
        {
           
            guideMain.LoadNext();
            SFXManager.instance.PlaySound(PageSound);
        }
    }
}
