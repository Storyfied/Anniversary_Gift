using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestProgressionField : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_questDescText = null;
    [SerializeField] private TextMeshProUGUI m_questProgressText = null;

    private Button m_button = null;
    private CanvasGroup m_canvasGroup = null;

    private void Awake()
    {
        m_button = GetComponent<Button>();
        m_canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Setup(Character _character, UnityEngine.Events.UnityAction _callBack)
    {
        m_button.onClick.AddListener(_callBack);
        UpdateProgress(_character);
    }

    public void UpdateProgress(Character _character)
    {
        m_questDescText.text = string.Format("<size=50>Complete {0}'s trivia <size=30>( {1} )</size>", _character.Name, 
            _character.IsTriviasCompleted ? "Done!" : "Click me!");

        if (_character.IsTriviasCompleted)
        {
            m_button.interactable = false;
            m_canvasGroup.alpha = 0.5f;
        }

        int CurrentProgress()
        {
            if (_character.IsTriviasCompleted)
                return _character.Trivias.Count;

            return _character.CurrentProgress;
        }

        m_questProgressText.text = string.Format("{0}/{1}", CurrentProgress(), _character.Trivias.Count);
    }
}
