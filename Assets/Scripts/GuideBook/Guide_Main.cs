using UnityEngine;

public class Guide_Main : MonoBehaviour
{
    public GameObject[] pages;
    public bool Can_Change = true;
    public int CurrentIndex = 0;

    [Header("Audio")]
    public AudioClip PageTurnSound;

    void Start()
    {
        foreach (var page in pages)
        {
            page.SetActive(false);
        }

        if (pages.Length > 0)
        {
            pages[0].SetActive(true);
        }
    }

    private void LoadPage(int index)
    {
        if (!Can_Change || index < 0 || index >= pages.Length)
        {
            Debug.LogWarning("Cannot change page or invalid page index: " + index);
            return;
        }

        if (CurrentIndex >= 0 && CurrentIndex < pages.Length)
        {
            pages[CurrentIndex].SetActive(false);
        }

        SFXManager.instance.PlaySound(PageTurnSound);
        
        pages[index].SetActive(true);
        CurrentIndex = index;
    }

    public void LoadDirect(int index)
    {
        LoadPage(index);
    }

    public void LoadNext()
    {
        if (Can_Change)
        {
            //Debug.Log("Test Forward");
            int nextIndex = CurrentIndex + 1;
            if (nextIndex < pages.Length)
            {
                LoadPage(nextIndex);
            }
        }
    }

    public void LoadPrevious()
    {
        if (Can_Change)
        {
            Debug.Log("Test Back");
            int previousIndex = CurrentIndex - 1;
            if (previousIndex >= 0)
            {
                LoadPage(previousIndex);
            }
        }
    }
}
