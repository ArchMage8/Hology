using UnityEngine;

public class InspectComponent : MonoBehaviour
{
    public GameObject Target_Page;  // The target page to be matched
    public bool positiveResponse;  // Determines if the response is positive or negative
    public GameObject componentHighlight;

    private void Awake()
    {
        componentHighlight.SetActive(false);
    }

}
