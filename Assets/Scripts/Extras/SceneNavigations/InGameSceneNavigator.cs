using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameSceneNavigator : MonoBehaviour
{
    public static InGameSceneNavigator Instance { get; private set; }

    public int MistakeLimitIndex;   // Scene index for mistake limit
    public int TimeLimitIndex;      // Scene index for time limit
    public int LevelSelectIndex;    // Scene index for level selection

    public GameObject completePrint;
    public Animator FadeAnimator;

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
    }

    // Function to load the scene for exceeding the mistake limit
    public void ExceedMistakeLimit()
    {
        LoadTargetScene(MistakeLimitIndex);
    }

    // Function to load the scene for exceeding the time limit
    public void ExceedTimeLimit()
    {
        LoadTargetScene(TimeLimitIndex);
    }

    // Function to finish level
    
    public void FinishLevel()
    {
        StartCoroutine(PrintFinish());
    }

    private IEnumerator PrintFinish()
    {
        completePrint.SetActive(true);
        yield return new WaitForSeconds(4f);
        LoadTargetScene(LevelSelectIndex);
    }

    private IEnumerator LoadTargetScene(int Target)
    {
        FadeAnimator.SetTrigger("EndScene");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(Target);
    }
}