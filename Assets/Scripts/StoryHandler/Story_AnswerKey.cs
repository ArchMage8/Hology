using UnityEngine;
using System.Collections.Generic;

public class Story_AnswerKey : MonoBehaviour
{
    public static Story_AnswerKey Instance;

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
    public class AnswerSet
    {
        public int storyIndex;
        public List<bool> answers; // true = accepted, false = rejected
    }

    public List<AnswerSet> correctAnswers = new List<AnswerSet>();
}
