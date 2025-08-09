using UnityEngine;
using UnityEngine.UI;

public class LevelChild : MonoBehaviour
{
    public int levelIndex; // Unique to this button's level
    public Button button;

    private void Start()
    {
        RefreshButtonState();
    }

    public void RefreshButtonState()
    {
        if (LevelManager.Instance != null)
        {
           if(levelIndex == LevelManager.Instance.currentLevelIndex)
           {
              UnlockButton();
           }

           else
           {
                LockButton();
           }
             
        }
    }

    private void LockButton()
    {

    }

    private void UnlockButton()
    {

    }
}
