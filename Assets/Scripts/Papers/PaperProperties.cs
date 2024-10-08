using UnityEngine;

public class PaperProperties : MonoBehaviour
{
    public bool Hoax;
    public GameObject ErrorPaper;
    public bool Lifestyle;
    public GameObject ResearchPaper;

    private void Start()
    {
        ErrorPaper.SetActive(false);

        if(Lifestyle == true)
        {
            ResearchPaper.SetActive(false);
        }
    }
}
