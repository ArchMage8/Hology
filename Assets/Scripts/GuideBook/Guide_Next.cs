using UnityEngine;

public class Guide_Next : MonoBehaviour
{
    public Guide_Main guideMain;

    void OnMouseDown()
    {
        if (guideMain != null)
        {
           
            guideMain.LoadNext();
        }
    }
}
