using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class NotifyDragInfo : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public delegate void DragAction(PointerEventData e);
    public event DragAction OnDragEvent;

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (OnDragEvent != null)
        {
            OnDragEvent(eventData);
        }
    }
}
