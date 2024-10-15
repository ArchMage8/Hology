using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSystem : MonoBehaviour
{
    public static MainMenuSystem Instance { get; private set; }

    public GameObject StartPaper;
    public GameObject CreditsPaper;

    public Animator FadeAnimator;
    public int LevelSelectIndex;
    [HideInInspector]public bool PaperActive;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Make sure the instance persists between scenes
        }
        else
        {
            Destroy(gameObject);  // Ensure only one instance exists
        }

        StartPaper.SetActive(false);
        CreditsPaper.SetActive(false);
    }

    public void StartGame()
    {
        if (!PaperActive)
        {
            PaperActive = true;
            StartPaper.SetActive(true);
            StartCoroutine(StartTheGame());
        }
    }

    public void ShowCredits()
    {
        if (!PaperActive)
        {
            PaperActive = true;
            CreditsPaper.SetActive(true);

        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator StartTheGame()
    {
        yield return new WaitForSeconds(6f);
        FadeAnimator.SetTrigger("EndScene");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(LevelSelectIndex);
    }

}
