using UnityEngine;

public class Guidebook_Ignore : MonoBehaviour
{
    public PolygonCollider2D bookCollider;
    private PolygonCollider2D selfCollider;

    private void Start()
    {
        selfCollider = GetComponent<PolygonCollider2D>();
        //polygonCollider = bookCollider.GetComponent<PolygonCollider2D>();
    }

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (selfCollider.bounds.Contains(mousePosition))
        {
            bookCollider.enabled = false;
        }
        else
        {
            bookCollider.enabled = true;
        }
    }
}
