using UnityEngine;

public class Guide_Previous : MonoBehaviour
{
    public Guide_Main guideMain;

    void OnMouseDown()
    {
        if (guideMain != null)
        {
            guideMain.LoadPrevious();
        }
    }
}
