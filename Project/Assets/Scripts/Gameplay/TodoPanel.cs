using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class TodoPanel : MonoBehaviour
{
    [Header("Internal References")]
    [SerializeField] private Button m_closeButton = null;
    [SerializeField] private TextMeshProUGUI m_title = null;
    [SerializeField] private Transform m_content = null;
    [SerializeField] private Transform m_questFieldsHolder = null;

    [Header("Prefab References")]
    [SerializeField] private QuestProgressionField m_questFieldPrefab = null;

    private Image m_darkOverlay = null;
    private List<QuestProgressionField> m_questFields = new List<QuestProgressionField>();

    private void Awake()
    {
        m_darkOverlay = GetComponent<Image>();

        // Spawn and cache QuestProgressionFields depending on amount of characters
        // TODO : Use object pooling to spawn these prefabs
        List<Character> characterList = GameManager.Instance.GetCharacterList();
        for (int i = 0; i < characterList.Count; i++)
        {
            Character character = characterList[i];
            QuestProgressionField newQuestField = Instantiate(m_questFieldPrefab, m_questFieldsHolder);

            newQuestField.Setup(character);
            m_questFields.Add(newQuestField);
        }
    }

    private void OnEnable()
    {
        m_closeButton.onClick.AddListener(() => Close());
        Setup();
    }

    private void OnDisable()
    {
        m_closeButton.onClick.RemoveListener(() => Close());
    }

    private void Close()
    {
        Sequence closeSequence = DOTween.Sequence().OnComplete(() => gameObject.SetActive(false));

        closeSequence.Append(m_darkOverlay.DOFade(0f, .3f));
        closeSequence.Join(m_content.DOScale(0f, .3f).SetEase(Ease.InBack));
    }

    public void Open()
    {
        if(m_darkOverlay == null)
            m_darkOverlay = GetComponent<Image>();

        m_darkOverlay.color = new Color(m_darkOverlay.color.r, m_darkOverlay.color.g, m_darkOverlay.color.b, 0f);
        m_content.localScale = Vector3.zero;

        gameObject.SetActive(true);

        Sequence openSequence = DOTween.Sequence();

        openSequence.Append(m_darkOverlay.DOFade(.5f, .3f));
        openSequence.Join(m_content.DOScale(1f, .3f).SetEase(Ease.OutBack));
    }

    public void Setup()
    {
        // Apply progress data into text fields
        for(int i = 0; i < m_questFields.Count; i++)
        {
            m_questFields[i].UpdateProgress(GameManager.Instance.GetCharacterList()[i]);
        }
    }
}
