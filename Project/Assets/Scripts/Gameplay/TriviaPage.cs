using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriviaPage : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI m_questionText = null;
    [SerializeField] private TextMeshProUGUI m_directoryText = null;
    [SerializeField] private Transform m_answerHolder = null;

    [Header("Prefabs")]
    [SerializeField] private TriviaAnswer m_answerPrefab = null;

    private Trivia m_currentTrivia = null;
    private List<TriviaAnswer> m_activeTriviaAnswerList = new List<TriviaAnswer>();

    public void Setup(Trivia _trivia, string _directoryString)
    {
        m_currentTrivia = _trivia;

        // Setup questions and answers
        m_questionText.text = m_currentTrivia.Question;
        for(int i = 0; i < m_currentTrivia.Answers.Count; i++)
        {
            TriviaAnswer newAnswerObj = Instantiate(m_answerPrefab, m_answerHolder);
            newAnswerObj.Setup(m_currentTrivia.Answers[i], i);

            m_activeTriviaAnswerList.Add(newAnswerObj);
        }

        // Setup directory text
        m_directoryText.text = _directoryString;
    }

    public void CleanUp()
    {
        for(int i = 0; i < m_activeTriviaAnswerList.Count; i++)
        {
            Destroy(m_activeTriviaAnswerList[i].gameObject);
        }
        m_activeTriviaAnswerList.Clear();
    }
}
