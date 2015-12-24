// Based on http://rbarraza.com/html5-canvas-pageflip/

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


[ExecuteInEditMode]
public class Flippable : MonoBehaviour, IDragHandler
{
    public RectTransform frontPageMask;
    public RectTransform shadow;
    public RectTransform backPage;
    public RectTransform backPageShadow;
    public RectTransform C;
    public RectTransform SB;
    public RectTransform ST;
    public RectTransform EB;

    public void OnDrag(PointerEventData e)
    {
//        C.position = Input.mousePosition;
//        UpdateGeometry();
    }

    void Start()
    {
        UpdateGeometry();
    }
    
    void Update()
    {
        UpdateGeometry();
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

    void UpdateGeometry()
    {
        transform.position =
            AssureInsideCircle(ST.position, (ST.position - EB.position).sqrMagnitude,
            AssureInsideCircle(SB.position, (SB.position - EB.position).sqrMagnitude, C.position));

        var T0 = (transform.position + EB.position) / 2;

        var u = Vector3.Normalize(Vector3.Cross((EB.position - transform.position), new Vector3(0, 0, -1)));
        var v = (SB.position - EB.position).normalized;
        var lhs = Vector3.Cross(v, u);
        var rhs = Vector3.Cross((T0 - EB.position), u);
        var b = rhs.z / lhs.z;
        var T1_ = EB.position + b * v;
        var T1 = new Vector3(T1_.x, T1_.y, 0);

        var delta = T1 - transform.position;
        var backContentAngle = 180 * Mathf.Atan2(delta.y, delta.x) / Mathf.PI;
        transform.rotation = Quaternion.Euler(0, 0, backContentAngle + 90);

        frontPageMask.position = T1;
        var frontPageMaskAngle = 180 * Mathf.Atan2(u.y, u.x) / Mathf.PI;
        frontPageMask.rotation = Quaternion.Euler(0, 0, frontPageMaskAngle);

        if (backPageShadow != null)
        {
            backPageShadow.position = T1;
            backPageShadow.rotation = Quaternion.Euler(0, 0, 180 + frontPageMaskAngle);
        }

        if (shadow != null)
        {
            shadow.position = T1;
            shadow.rotation = Quaternion.Euler(0, 0, frontPageMaskAngle);
        }
    }
}
