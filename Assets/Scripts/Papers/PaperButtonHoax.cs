using UnityEngine;

public class PaperButtonHoax : MonoBehaviour
{
    private void OnMouseDown()
    {
        // Call NextPaper from PaperManager and pass true (indicating it's a hoax)
        PaperManager.Instance.NextPaper(true);
    }
}
