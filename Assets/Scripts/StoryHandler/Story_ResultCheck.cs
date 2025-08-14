using UnityEngine;

public class Story_ResultCheck : MonoBehaviour
{
    public int storylineIndex;                  // Which storyline this ResultCheck checks
    public GameObject Paper_ResponseA;          // Shown if player matches answer key
    public GameObject Paper_ResponseB;          // Shown if player does not match

    private void OnEnable()
    {
        // Disable both responses initially
        if (Paper_ResponseA != null) Paper_ResponseA.SetActive(false);
        if (Paper_ResponseB != null) Paper_ResponseB.SetActive(false);

        // Check result and enable the correct paper
        bool result = CheckResult();

        if (result)
        {
            if (Paper_ResponseA != null)
            {
                Paper_ResponseA.SetActive(true);
                Paper_ResponseB.SetActive(false);
            }
        }
        else
        {
            if (Paper_ResponseB != null)
            {
                Paper_ResponseA.SetActive(false);
                Paper_ResponseB.SetActive(true);
            }
        }
    }

    private bool CheckResult()
    {
        if (Story_AnswerKey.Instance == null || Story_DecisionsHolder.Instance == null)
        {
            Debug.LogWarning("AnswerKey or DecisionsHolder instance not found.");
            return false;
        }

        var answerKeyList = Story_AnswerKey.Instance.correctAnswers;
        var playerList = Story_DecisionsHolder.Instance.playerDecisions;

        if (storylineIndex < 0 ||
            storylineIndex >= answerKeyList.Count ||
            storylineIndex >= playerList.Count)
        {
            Debug.LogWarning($"Invalid storyline index: {storylineIndex}");
            return false;
        }

        var correctList = answerKeyList[storylineIndex].answers;
        var playerDecisions = playerList[storylineIndex].answers;

        if (correctList.Count != playerDecisions.Count)
            return false;

        for (int i = 0; i < correctList.Count; i++)
        {
            if (correctList[i] != playerDecisions[i])
                return false;
        }

        return true; // Exact match
    }
}
