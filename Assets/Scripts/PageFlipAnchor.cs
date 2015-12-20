using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PageFlipAnchor : MonoBehaviour
{
    public enum AnchorType
    {
        LEFT_TOP,
        RIGHT_TOP,
        LEFT_BOTTOM,
        RIGHT_BOTTOM
    }

    public RectTransform targetPage;
    public AnchorType anchorType;

    void Awake()
    {
        if (Application.isPlaying)
        {
            Destroy(this);
        }
    }

    void Update ()
    {
        var pivot = targetPage.pivot;
        Vector3 local = new Vector3();

        switch (anchorType)
        {
            case AnchorType.LEFT_TOP:
                local.x = 0;
                local.y = targetPage.rect.height;
                break;

            case AnchorType.RIGHT_TOP:
                local.x = targetPage.rect.width;
                local.y = targetPage.rect.height;
                break;

            case AnchorType.LEFT_BOTTOM:
                local.x = local.y = 0;
                break;

            case AnchorType.RIGHT_BOTTOM:
                local.x = targetPage.rect.width;
                local.y = 0;
                break;
        }

        local.x -= pivot.x * targetPage.rect.width;
        local.y -= pivot.y * targetPage.rect.height;

        transform.position = targetPage.TransformPoint(local);
    }
}
