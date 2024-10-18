using UnityEngine;

public class Guidebook_Ignore : MonoBehaviour
{
    public GameObject bookCollider;
    private PolygonCollider2D polygonCollider;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        polygonCollider = bookCollider.GetComponent<PolygonCollider2D>();
    }

    private void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (boxCollider.bounds.Contains(mousePosition))
        {
            polygonCollider.enabled = false;
        }
        else
        {
            polygonCollider.enabled = true;
        }
    }
}
