using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestProgressionField : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_questDescText = null;
    [SerializeField] private TextMeshProUGUI m_questProgressText = null;

    private Button m_button;

    private void Awake()
    {
        m_button = GetComponent<Button>();
    }

    public void Setup(Character _character, UnityEngine.Events.UnityAction _callBack)
    {
        m_button.onClick.AddListener(_callBack);

        m_questDescText.text = string.Format("<size=50>Complete {0}'s trivia <size=30>( Click me! )</size>", _character.Name);
        UpdateProgress(_character);
    }

    public void UpdateProgress(Character _character)
    {
        m_questProgressText.text = string.Format("{0}/{1}", _character.CurrentProgress, _character.Trivias.Count);
    }
}
