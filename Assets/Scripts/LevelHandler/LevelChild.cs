using UnityEngine;
using UnityEngine.UI;

public class LevelChild : MonoBehaviour
{
    public int levelIndex; // Unique to this button's level
    public Collider2D detect_collider;

    [Space(10)]
    [Header("Locked Visuals")]

    public GameObject StrikeVisual;
    public GameObject CrossVisual;


    private void Start()
    {
        RefreshButtonState();
    }

    public void RefreshButtonState()
    {
        if (LevelManager.Instance != null)
        {
           if(levelIndex < LevelManager.Instance.currentLevelIndex)
           {
                Completed();
           }

           else if(levelIndex == LevelManager.Instance.currentLevelIndex)
           {
                Unlock();
           }

           else if(levelIndex > LevelManager.Instance.currentLevelIndex)
           {
                Lock();
           }
             
        }
    }

    public void Unlock()
    {
        detect_collider.enabled = true;
        StrikeVisual.SetActive(false);
        CrossVisual.SetActive(false);
    }

    public void Completed()
    {
        detect_collider.enabled = false;
        StrikeVisual.SetActive(false);
        CrossVisual.SetActive(true);
    }

    public void Lock()
    {
        detect_collider.enabled = false;
        StrikeVisual.SetActive(true);
        CrossVisual.SetActive(false);
    }

   
}
