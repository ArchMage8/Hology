using UnityEngine;

public class Guide_Main : MonoBehaviour
{
    public GameObject[] pages;
    public int CurrentIndex = 0;

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
        if (index < 0 || index >= pages.Length)
        {
            Debug.LogWarning("Invalid page index: " + index);
            return;
        }

        if (CurrentIndex >= 0 && CurrentIndex < pages.Length)
        {
            pages[CurrentIndex].SetActive(false);
        }

        pages[index].SetActive(true);
        CurrentIndex = index;
    }

    public void LoadDirect(int index)
    {
        LoadPage(index);
    }

    public void LoadNext()
    {
        int nextIndex = CurrentIndex + 1;

        if (nextIndex < pages.Length)
        {
            LoadPage(nextIndex);
        }
    }

    public void LoadPrevious()
    {
        int previousIndex = CurrentIndex - 1;

        if (previousIndex >= 0)
        {
            LoadPage(previousIndex);
        }
    }
}
