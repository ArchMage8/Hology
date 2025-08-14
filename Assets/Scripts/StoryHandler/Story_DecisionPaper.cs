using UnityEngine;

public class Story_DecisionPaper : MonoBehaviour
{
    public int storylineID;
    public int decisionIndex;

    // Pass in hoax (true = rejected in old system) ? convert to accepted boolean
    public void RecordDecision(bool hoax)
    {
        // hoax == true -> rejected -> isAccepted = false
        // hoax == false -> accepted -> isAccepted = true
        bool isAccepted = !hoax;
        Story_DecisionsHolder.Instance.AddDecision(storylineID, decisionIndex, isAccepted);
    }
}
