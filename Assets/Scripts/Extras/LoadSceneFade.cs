using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneFade : MonoBehaviour
{
    public Animator animator;
    private int DestScene;

    public void ChangeSceneWithFade(int DestinationScene)
    {
        DestScene = DestinationScene;
        StartCoroutine(LoadDestScene());
    }

    private IEnumerator LoadDestScene()
    {
        animator.SetTrigger("EndScene");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(DestScene);
    }

}
