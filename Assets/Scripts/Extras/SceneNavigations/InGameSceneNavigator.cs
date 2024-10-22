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
    public float completeDelay = 3f;

    [Space(10)]
    [Header("Audio")]
    public AudioClip PrinterSound;
    public SFXManager_Exception PrinterSoundPlayer;

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
        StartCoroutine(LoadTargetScene(MistakeLimitIndex));
    }

    // Function to load the scene for exceeding the time limit
    public void ExceedTimeLimit()
    {
        StartCoroutine(LoadTargetScene(TimeLimitIndex));
    }

    // Function to finish level
    
    public void FinishLevel()
    {
        StartCoroutine(PrintFinish());
    }

    private IEnumerator PrintFinish()
    {
        completePrint.SetActive(true);
        PrinterSoundPlayer.PlaySound(PrinterSound);
        yield return new WaitForSeconds(5.2f);
        StartCoroutine(LoadTargetScene(LevelSelectIndex));
    }

    private IEnumerator LoadTargetScene(int Target)
    {
        FadeAnimator.SetTrigger("EndScene");
        yield return new WaitForSeconds(completeDelay);
        Debug.Log("End Level");
        SceneManager.LoadScene(Target);
    }
}
