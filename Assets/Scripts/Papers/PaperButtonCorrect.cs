using UnityEngine;

public class PaperButtonCorrect : MonoBehaviour
{
    private void OnMouseDown()
    {
        // Call NextPaper from PaperManager and pass false (indicating it's correct, not a hoax)
        PaperManager.Instance.NextPaper(false);
    }
}
