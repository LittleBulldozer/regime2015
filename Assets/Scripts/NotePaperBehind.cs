using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class NotePaperBehind : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Vector2 prevMousePos;

    // DRAG
    public void OnBeginDrag(PointerEventData eventData)
    {
        prevMousePos = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransform T = GetComponent<RectTransform>();
        Vector2 delta = (new Vector2(Input.mousePosition.x, Input.mousePosition.y)) - prevMousePos;
        T.Rotate(0, 0, -0.5f * delta.x);
        T.position += new Vector3(0, delta.y, 0);
        prevMousePos = Input.mousePosition;
    }
}
