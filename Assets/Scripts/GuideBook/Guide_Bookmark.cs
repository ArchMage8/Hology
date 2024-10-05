using UnityEngine;

public class Guide_Bookmark : MonoBehaviour
{
    public Guide_Main guideMain;
    public int bookmarkIndex;

    void OnMouseDown()
    {
        if (guideMain != null)
        {
            guideMain.LoadDirect(bookmarkIndex);
        }
    }
}
