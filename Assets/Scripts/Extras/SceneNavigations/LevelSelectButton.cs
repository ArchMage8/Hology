using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectButton : MonoBehaviour
{
    public GameObject hoverEffect;
    public int TargetScene;
    public Animator FadeAnimator;

    private void OnMouseEnter()
    {
        hoverEffect.SetActive(true);
    }

    private void OnMouseExit()
    {
        hoverEffect.SetActive(false);
    }

    private void OnMouseDown()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        FadeAnimator.SetTrigger("EndScene");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(TargetScene);
    }
}
