using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public Sprite originalSprite;
    public Sprite hoverSprite;
    public AudioClip ButtonSound;

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
        SFXManager.instance.PlaySound();
        MainMenuSystem.Instance.StartGame();
    }
}
