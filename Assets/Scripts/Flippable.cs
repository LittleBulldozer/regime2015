// Based on http://rbarraza.com/html5-canvas-pageflip/

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class Flippable : MonoBehaviour
{
    public RectTransform frontPageMask;
    public RectTransform backPage;
    public RectTransform backPageShadow;
    public RectTransform frontPageBackSide;
    public RectTransform frontPageBackSideShadow;
    public RectTransform C;
    public RectTransform SB;
    public RectTransform ST;
    public RectTransform EB;

    NotifyDragInfo notifyDragInfo;

    void Awake()
    {
        frontPageBackSide.gameObject.AddComponent<NotifyDragInfo>();
        notifyDragInfo = frontPageBackSide.gameObject.GetComponent<NotifyDragInfo>();
        notifyDragInfo.OnDragEvent += OnBackContentDrag;
    }

    void Start()
    {
        UpdateGeometry(C.position);
    }

    void OnDestroy()
    {
        DestroyImmediate(notifyDragInfo);
    }

    void OnBackContentDrag(PointerEventData e)
    {
        UpdateGeometry(Input.mousePosition);
    }

    Vector2 AssureInsideCircle(Vector2 Origin, float distSquare, Vector2 samplePos)
    {
        var deltaPos = samplePos - Origin;
        if (deltaPos.sqrMagnitude > distSquare)
        {
            var angle = Mathf.Atan2(deltaPos.y, deltaPos.x);
            var dist = Mathf.Sqrt(distSquare);
            return new Vector2(
                Origin.x + Mathf.Cos(angle) * dist,
                Origin.y + Mathf.Sin(angle) * dist);
        }
        else
        {
            return samplePos;
        }
    }

    void UpdateGeometry(Vector2 samplePos)
    {
        C.position = frontPageBackSide.position =
            AssureInsideCircle(ST.position, (ST.position - EB.position).sqrMagnitude,
            AssureInsideCircle(SB.position, (SB.position - EB.position).sqrMagnitude, samplePos));

        var T0 = (C.position + EB.position) / 2;

        var u = Vector3.Normalize(Vector3.Cross((EB.position - C.position), new Vector3(0, 0, -1)));
        var v = (SB.position - EB.position).normalized;
        var lhs = Vector3.Cross(v, u);
        var rhs = Vector3.Cross((T0 - EB.position), u);
        var b = rhs.z / lhs.z;
        var T1_ = EB.position + b * v;
        var T1 = new Vector3(T1_.x, T1_.y, 0);

        var delta = T1 - C.position;
        var backContentAngle = 180 * Mathf.Atan2(delta.y, delta.x) / Mathf.PI;
        frontPageBackSide.rotation = Quaternion.Euler(0, 0, backContentAngle + 90);

//        backPage.position;

        frontPageMask.position = T1;
        var frontPageMaskAngle = 180 * Mathf.Atan2(u.y, u.x) / Mathf.PI;
        frontPageMask.rotation = Quaternion.Euler(0, 0, frontPageMaskAngle);

        backPageShadow.position = T1;
        backPageShadow.rotation = Quaternion.Euler(0, 0, 180 + frontPageMaskAngle);

        frontPageBackSideShadow.position = T1;
        frontPageBackSideShadow.rotation = Quaternion.Euler(0, 0, frontPageMaskAngle);
    }
}
