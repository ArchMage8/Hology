using UnityEngine;

public class Guide_Previous : MonoBehaviour
{
    public Guide_Main guideMain;
    [Header("Audio")]
    public AudioClip PageSound;

    void OnMouseDown()
    {
       
        if (guideMain != null)
        {
            
            guideMain.LoadPrevious();
            SFXManager.instance.PlaySound(PageSound);
        }
    }
}
