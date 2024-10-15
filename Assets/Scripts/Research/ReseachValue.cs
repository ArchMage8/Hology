using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReseachValue : MonoBehaviour
{
    public Sprite originalSprite;
    public Sprite hoverSprite;
    public AudioClip ButtonSound;

    public string digit;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = originalSprite;
    }

    public void OnMouseDown()
    {
        if (ResearchCode.Instance != null && !GameStateHandler.instance.isInspecting && !GameStateHandler.instance.isPrinting)
        {
            SFXManager.instance.PlaySound(ButtonSound);
            ResearchCode.Instance.AddDigit(digit);
        }
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
}
