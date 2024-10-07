using UnityEngine;

public class Guide_Drag : MonoBehaviour
{
    public GameObject[] bookmarks;  // Array of GameObjects with BoxColliders (bookmarks)
    public GameObject[] navigationButtons;  // Array of GameObjects (navigation buttons)
    public GameObject target;  // Main GameObject with BoxCollider (Target)
    public Collider2D draggableArea;  // Collider representing the draggable area

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
                button.SetActive(false);
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
                button.SetActive(true);
            }
        }
    }

    Vector3 ClampToDraggableArea(Vector3 targetPosition)
    {
        if (draggableArea != null)
        {
            Bounds bounds = draggableArea.bounds;

            float clampedX = Mathf.Clamp(targetPosition.x, bounds.min.x, bounds.max.x);
            float clampedY = Mathf.Clamp(targetPosition.y, bounds.min.y, bounds.max.y);

            targetPosition = new Vector3(clampedX, clampedY, targetPosition.z);
        }

        return targetPosition;
    }
}
