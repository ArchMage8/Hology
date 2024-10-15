using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCredits : MonoBehaviour
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
        SFXManager.instance.PlaySound(ButtonSound);
        MainMenuSystem.Instance.ShowCredits();
    }
}
