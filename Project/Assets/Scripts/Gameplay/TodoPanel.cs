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
    [SerializeField] private RectTransform m_content = null;
    [SerializeField] private RectTransform m_pagesContent = null;
    [SerializeField] private RectTransform m_pagesScrollView = null;
    [SerializeField] private GameObject m_inputBlocker = null;

    [Header("Main Page References")]
    [SerializeField] private Transform m_questFieldsHolder = null;

    [Header("Trivia Page References")]
    [SerializeField] private TriviaPage m_triviaPage = null;
    [SerializeField] private Button m_triviaBackButton = null;

    [Header("Prefab References")]
    [SerializeField] private QuestProgressionField m_questFieldPrefab = null;

    private Image m_darkOverlay = null;
    private List<QuestProgressionField> m_questFields = new List<QuestProgressionField>();
    private string[] m_titleStrings = { "To-Do List O", "{0}'s Trivia O" };
    private float[] m_pageViewHeights = { 892f, 742f };
    private float[] m_pageHeights = { 1100f, 950f };

    private void Awake()
    {
        m_darkOverlay = GetComponent<Image>();

        // Disable input blocker by default
        m_inputBlocker.SetActive(false);

        // Spawn and cache QuestProgressionFields depending on amount of characters
        // TODO : Use object pooling to spawn these prefabs
        List<Character> characterList = GameManager.Instance.GetCharacterList();
        for (int i = 0; i < characterList.Count; i++)
        {
            Character character = characterList[i];
            QuestProgressionField newQuestField = Instantiate(m_questFieldPrefab, m_questFieldsHolder);

            newQuestField.Setup(character, () => ChangePage(PAGE_TYPE.TRIVIA, character));
            m_questFields.Add(newQuestField);
        }

        m_triviaBackButton.onClick.AddListener(() => ChangePage(PAGE_TYPE.MAIN));
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

    private void ChangePage(PAGE_TYPE _page, Character _character = null)
    {
        m_inputBlocker.SetActive(true);

        // Setup page
        switch(_page)
        {
            case PAGE_TYPE.MAIN:
                m_title.text = m_titleStrings[(int)_page];
                m_triviaPage.CleanUp();
                break;

            case PAGE_TYPE.TRIVIA:
                m_title.text = string.Format(m_titleStrings[(int)_page], _character.Name);
                m_triviaPage.Setup(_character.GetCurrentTrivia(), string.Format("{0}/{1}", _character.CurrentProgress + 1, _character.Trivias.Count));
                break;
        }

        // Scroll to target page
        float targetPos = m_pagesContent.GetChild((int)_page).transform.localPosition.x;
        float contentPos = m_pagesContent.transform.localPosition.x;
        DOTween.To(() => contentPos, x => contentPos = x, -targetPos, .3f)
            .OnUpdate(() => {
                m_pagesContent.localPosition = new Vector3(contentPos, 0f, 0f);
            })
            .OnComplete(() => m_inputBlocker.SetActive(false));

        // Tween page view height
        float targetViewHeight = m_pageViewHeights[(int)_page];
        m_pagesScrollView.DOSizeDelta(new Vector2(m_pagesScrollView.sizeDelta.x, targetViewHeight), .2f);

        // Tween page height
        float targetPageHeight = m_pageHeights[(int)_page];
        m_content.DOSizeDelta(new Vector2(m_content.sizeDelta.x, targetPageHeight), .2f);
    }
}