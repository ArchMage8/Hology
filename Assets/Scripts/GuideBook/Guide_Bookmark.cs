using UnityEngine;

public class Guide_Bookmark : MonoBehaviour
{
    public Guide_Main guideMain;
    public int bookmarkIndex;

    [Header("Audio")]
    public AudioClip PageSound;

    void OnMouseDown()
    {
        if (guideMain != null)
        {
            guideMain.LoadDirect(bookmarkIndex);
            SFXManager.instance.PlaySound(PageSound);
        }
    }
}
