using UnityEngine;

public class Guide_Drag : MonoBehaviour
{
    [Header("Bookmarks and Navigation Buttons")]
    public GameObject[] bookmarks;  // Array of GameObjects with BoxColliders (bookmarks)
    public GameObject[] navigationButtons;  // Array of GameObjects (navigation buttons)

    [Header("Target Object")]
    public GameObject target;  // Main GameObject with BoxCollider (Target)

    [Header("Draggable Area Boundaries")]
    public float minX;  // Minimum X coordinate of the draggable area
    public float maxX;  // Maximum X coordinate of the draggable area
    public float minY;  // Minimum Y coordinate of the draggable area
    public float maxY;  // Maximum Y coordinate of the draggable area

    private bool isDragging = false;
    private Vector3 offset;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D targetCollider = target.GetComponent<Collider2D>();

            if (targetCollider != null && targetCollider.OverlapPoint(mousePosition))
            {
                PickUpTarget(mousePosition);
            }
        }

        if (isDragging)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 targetPosition = mousePosition + offset;

                // Limit the target position within the draggable area bounds
                targetPosition = ClampToDraggableArea(targetPosition);

                target.transform.position = targetPosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                DropTarget();
            }
        }
    }

    void PickUpTarget(Vector3 mousePosition)
    {
        isDragging = true;
        offset = target.transform.position - mousePosition;

        foreach (var bookmark in bookmarks)
        {
            if (bookmark != null)
            {
                bookmark.GetComponent<Collider2D>().enabled = false;
            }
        }

        foreach (var button in navigationButtons)
        {
            if (button != null)
            {
                button.GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    void DropTarget()
    {
        isDragging = false;

        foreach (var bookmark in bookmarks)
        {
            if (bookmark != null)
            {
                bookmark.GetComponent<Collider2D>().enabled = true;
            }
        }

        foreach (var button in navigationButtons)
        {
            if (button != null)
            {
                button.GetComponent<Collider2D>().enabled = true;
            }
        }
    }

    Vector3 ClampToDraggableArea(Vector3 targetPosition)
    {
        // Clamp the target position within the defined min and max X, Y range
        float clampedX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(targetPosition.y, minY, maxY);

        targetPosition = new Vector3(clampedX, clampedY, targetPosition.z);
        return targetPosition;
    }
}
