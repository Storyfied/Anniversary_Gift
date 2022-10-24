using UnityEngine;

public class Bunny : MonoBehaviour
{
    [SerializeField] private GameObject m_textHolder = null;
    [SerializeField] private TextBubble m_textBubble = null;

    private void Awake()
    {
        m_textBubble.gameObject.SetActive(false);
    }

    public void ShowTextBubble(string _message)
    {
        m_textBubble.gameObject.SetActive(true);
        m_textBubble.ShowBubble(_message);
    }

    public void HideTextBubble()
    {
        m_textBubble.HideBubble(() => m_textBubble.gameObject.SetActive(false));
    }

    public void EnableIdleText()
    {
        m_textHolder.SetActive(true);
    }

    public void DisableIdleText()
    {
        m_textHolder.SetActive(false);
    }
}
