using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class TriviaPage : MonoBehaviour
{
    public delegate void OnLoadNewTrivia();
    public OnLoadNewTrivia OnLoadNewTriviaHandler = null;
    public OnLoadNewTrivia OnLoadedNewTriviaHandler = null;

    public delegate void OnCharacterTriviaCompleted();
    public OnCharacterTriviaCompleted OnCharacterTriviaCompletedHandler = null;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI m_questionText = null;
    [SerializeField] private TextMeshProUGUI m_directoryText = null;
    [SerializeField] private Transform m_answerHolder = null;
    [SerializeField] private Image m_completedStampImage = null;

    [Header("Prefabs")]
    [SerializeField] private TriviaAnswer m_answerPrefab = null;

    private Character m_currentCharacter = null;
    private CanvasGroup m_canvasGroup = null;
    private List<TriviaAnswer> m_activeTriviaAnswerList = new List<TriviaAnswer>();

    private void Awake()
    {
        m_canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Setup(Character _character)
    {
        m_currentCharacter = _character;
        Trivia currentTrivia = m_currentCharacter.GetCurrentTrivia();

        // Setup questions and answers
        m_questionText.text = currentTrivia.Question;
        for(int i = 0; i < currentTrivia.Answers.Count; i++)
        {
            TriviaAnswer newAnswerObj = Instantiate(m_answerPrefab, m_answerHolder);
            newAnswerObj.Setup(currentTrivia.Answers[i], i, (answerIndex) => CheckAnswer(answerIndex));

            m_activeTriviaAnswerList.Add(newAnswerObj);
        }

        // Setup directory text
        m_directoryText.text = string.Format("{0}/{1}", _character.CurrentProgress + 1, _character.Trivias.Count);

        if(m_completedStampImage.gameObject.activeInHierarchy)
            m_completedStampImage.gameObject.SetActive(false);

        m_canvasGroup.alpha = 1f;
    }

    public void CleanUp()
    {
        for(int i = 0; i < m_activeTriviaAnswerList.Count; i++)
        {
            Destroy(m_activeTriviaAnswerList[i].gameObject);
        }
        m_activeTriviaAnswerList.Clear();
    }

    private void CheckAnswer(int _answerIndex)
    {
        int correctAnswerIndex = m_currentCharacter.GetCurrentTrivia().CorrectAnswer;
        if(_answerIndex == correctAnswerIndex)
        {
            m_currentCharacter.UpdateCurrentProgress();
            StartCoroutine(LoadNextTrivia());
        }
    }

    private IEnumerator LoadNextTrivia()
    {
        OnLoadNewTriviaHandler?.Invoke();

        if (m_currentCharacter.IsTriviasCompleted)
        {
            m_completedStampImage.color = new Color(1f, 1f, 1f, 0f);
            m_completedStampImage.transform.localScale = new Vector3(.3f, .3f, .3f);
            m_completedStampImage.gameObject.SetActive(true);

            // Play stamp animation
            m_canvasGroup.DOFade(.75f, .15f);
            m_completedStampImage.DOFade(1f, .2f);
            m_completedStampImage.transform.DOScale(.15f, .3f).SetEase(Ease.OutBack);

            yield return new WaitForSeconds(1f);

            OnCharacterTriviaCompletedHandler?.Invoke();
            OnLoadedNewTriviaHandler?.Invoke();
            yield break;
        }

         m_canvasGroup.DOFade(0f, .3f).OnComplete(() =>
         {
             CleanUp();
             Setup(m_currentCharacter);
             m_canvasGroup.alpha = 0f;
         });

        yield return new WaitForSeconds(0.35f);

        m_canvasGroup.DOFade(1f, .3f).OnComplete(() => OnLoadedNewTriviaHandler?.Invoke());

        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
    }
}
