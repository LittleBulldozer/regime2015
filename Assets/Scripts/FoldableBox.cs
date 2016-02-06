using UnityEngine;
using System.Collections;

public class FoldableBox : MonoBehaviour
{
    public float foldWidth;

    float normalWidth;

    enum State
    {
        Normal,
        Fold
    }

    State state = State.Normal;

	// Use this for initialization
	void Awake ()
    {
        var T = GetComponent<RectTransform>();
        normalWidth = T.rect.width;
    }

    public void ToggleFold()
    {
        var T = GetComponent<RectTransform>();

        switch (state)
        {
            case State.Normal:
                state = State.Fold;
                T.sizeDelta = new Vector2(foldWidth, T.rect.height);
//                T.rect.Set(T.rect.x, T.rect.y, foldWidth, T.rect.height);
                break;

            case State.Fold:
                state = State.Normal;
                T.sizeDelta = new Vector2(normalWidth, T.rect.height);
//                T.rect.Set(T.rect.x, T.rect.y, normalWidth, T.rect.height);
                break;

            default:
                throw new System.Exception("Unhandled state : " + state);
        }
    }
}
