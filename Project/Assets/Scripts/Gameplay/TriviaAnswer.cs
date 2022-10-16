using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TriviaAnswer : MonoBehaviour
{
    [SerializeField] private Button m_button = null;
    [SerializeField] private TextMeshProUGUI m_text = null;

    private char[] alphabetList = { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };

    public int AnswerIndex { get; private set; }

    public void Setup(string _answerString, int _index)
    {
        m_text.text = string.Format("{0}. {1}", alphabetList[_index], _answerString);
        //m_button.onClick.AddListener();

        AnswerIndex = _index;
    }
}
