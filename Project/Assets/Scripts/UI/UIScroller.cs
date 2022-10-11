using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScroller : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float m_horizontalScrollSpeed = 0f;
    [SerializeField] private float m_verticalScrollSpeed = 0f;

    private RawImage m_rawImage = null;

    private void Awake()
    {
        m_rawImage = GetComponent<RawImage>();
    }

    private void Update()
    {
        m_rawImage.uvRect = new Rect(m_rawImage.uvRect.position + new Vector2(m_horizontalScrollSpeed, m_verticalScrollSpeed) * Time.deltaTime, m_rawImage.uvRect.size);
    }
}
