using UnityEngine;
using DG.Tweening;
using TMPro;

public class TextBubble : MonoBehaviour
{
    [SerializeField] private Transform m_transform = null;
    [SerializeField] private TextMeshProUGUI m_text = null;

    public void ShowBubble(string _message)
    {
        m_transform.localScale = Vector3.zero;
        m_text.text = _message;

        // Play animation
        m_transform.DOScale(1f, .3f).SetEase(Ease.OutBack);
    }

    public void HideBubble(System.Action _callback)
    {
        // Play animation
        m_transform.DOScale(0f, .3f).SetEase(Ease.InBack)
            .OnComplete(() => _callback?.Invoke());
    }
}
