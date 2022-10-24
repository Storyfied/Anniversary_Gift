using UnityEngine;
using UnityEngine.EventSystems;

public class InputBlocker : MonoBehaviour, IPointerClickHandler
{
    public delegate void OnInputDetected();
    public OnInputDetected OnInputDetectedHandler = null;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnInputDetectedHandler?.Invoke();
    }
}
