using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TodoPanel : MonoBehaviour
{
    [SerializeField] private Button m_closeButton = null;

    private Image m_darkOverlay = null;

    private void Awake()
    {
        m_darkOverlay = GetComponent<Image>();
    }

    private void OnEnable()
    {
        m_closeButton.onClick.AddListener(() => Close());

        m_darkOverlay.color = new Color(m_darkOverlay.color.r, m_darkOverlay.color.g, m_darkOverlay.color.b, 0f);
        m_darkOverlay.DOFade(.5f, .3f);
    }

    private void OnDisable()
    {
        m_closeButton.onClick.RemoveListener(() => Close());
    }

    private void Close()
    {
        m_darkOverlay.DOFade(0f, .3f)
            .OnComplete(() => gameObject.SetActive(false));

        Debug.Log("closing todo list!");
    }

    public void Setup()
    {

    }
}
