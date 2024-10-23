using UnityEngine;

public class Guide_Next : MonoBehaviour
{
    public Guide_Main guideMain;
    [Header("Audio")]
    public AudioClip PageSound;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if(guideMain.CurrentIndex == guideMain.pages.Length - 1)
        {
            spriteRenderer.enabled = false;
            boxCollider2D.enabled = false;
        }

        else
        {
            spriteRenderer.enabled = true;
            boxCollider2D.enabled = true;
        }
    }

    void OnMouseDown()
    {
        if (guideMain != null)
        {
           
            guideMain.LoadNext();
            SFXManager.instance.PlaySound(PageSound);
        }
    }
}
