using UnityEngine;
using System.Collections.Generic;

public class Story_DecisionsHolder : MonoBehaviour
{
    public static Story_DecisionsHolder Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    public class PlayerDecisionSet
    {
        public int storyIndex;
        public List<bool> answers = new List<bool>(); // true = accepted, false = rejected
    }

    public List<PlayerDecisionSet> playerDecisions = new List<PlayerDecisionSet>();

    public void AddDecision(int storyIndex, int decisionIndex, bool isAccepted)
    {
        PlayerDecisionSet set = playerDecisions.Find(s => s.storyIndex == storyIndex);

        if (set == null)
        {
            set = new PlayerDecisionSet
            {
                storyIndex = storyIndex
            };

            while (set.answers.Count <= decisionIndex)
            {
                set.answers.Add(false);
            }

            set.answers[decisionIndex] = isAccepted;
            playerDecisions.Add(set);
        }
        else
        {
            while (set.answers.Count <= decisionIndex)
            {
                set.answers.Add(false);
            }

            set.answers[decisionIndex] = isAccepted;
        }
    }
}
