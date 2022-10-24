using UnityEngine;

public class TiltController : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed = 0f;

    private Vector2 m_initPosition = Vector2.zero;

    private void Awake()
    {
        m_initPosition = transform.localPosition;
    }

    private void Update()
    {
        Vector2 tiltValue = new Vector2(Mathf.Clamp(Input.acceleration.x, -5f, 5f) * m_moveSpeed, (Mathf.Clamp(Input.acceleration.y, -5f, 5f) * m_moveSpeed));
        transform.localPosition = new Vector2(Mathf.Clamp(transform.localPosition.x + tiltValue.x, m_initPosition.x - 20f, m_initPosition.x + 20f),
                                                    Mathf.Clamp(transform.localPosition.y + tiltValue.y, m_initPosition.y, m_initPosition.y + 20f));
    }
}
