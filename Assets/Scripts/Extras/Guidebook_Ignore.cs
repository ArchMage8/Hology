using UnityEngine;

public class Guidebook_Ignore : MonoBehaviour
{
    public GameObject bookCollider;
    private PolygonCollider2D polygonCollider;
    private PolygonCollider2D selfCollider;

    private void Start()
    {
        selfCollider = GetComponent<PolygonCollider2D>();
        polygonCollider = bookCollider.GetComponent<PolygonCollider2D>();
    }

    private void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (selfCollider.bounds.Contains(mousePosition))
        {
            polygonCollider.enabled = false;
        }
        else
        {
            polygonCollider.enabled = true;
        }
    }
}
