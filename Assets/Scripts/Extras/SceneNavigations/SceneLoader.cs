using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Sprite originalSprite;
    public Sprite hoverSprite;
    public int LevelSelectIndex;
    public Animator FadeAnimator;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = originalSprite;
    }

    private void OnMouseEnter()
    {
        if (spriteRenderer != null && hoverSprite != null)
        {
            spriteRenderer.sprite = hoverSprite;
        }
    }


    private void OnMouseExit()
    {
        if (spriteRenderer != null && originalSprite != null)
        {
            spriteRenderer.sprite = originalSprite;
        }
    }

    private void OnMouseDown()
    {
        StartCoroutine(LevelSelect());
    }

    private IEnumerator LevelSelect()
    {
        FadeAnimator.SetTrigger("EndScene");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(LevelSelectIndex);
    }
}
