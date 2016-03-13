using UnityEngine;
using System.Collections;

public class CardCanvas : MonoBehaviour
{
    public CardView cardLT;
    public CardView cardRT;
    public CardView cardLB;
    public CardView cardRB;

    public CardPool.CardContext [] contexts;

    public CardPool.CardContext selectedCard
    {
        get {
            if (IsSelected(cardLT))
            {
                return contexts[0];
            }

            if (IsSelected(cardRT))
            {
                return contexts[1];
            }

            if (IsSelected(cardLB))
            {
                return contexts[2];
            }

            if (IsSelected(cardRB))
            {
                return contexts[3];
            }

            return null;
        }
    }

    public void ApplyView(CardPool.CardContext [] contexts)
    {
        this.contexts = contexts;

        ApplyView(cardLT, contexts[0]);
        ApplyView(cardRT, contexts[1]);
        ApplyView(cardLB, contexts[2]);
        ApplyView(cardRB, contexts[3]);
    }

    void ApplyView(CardView view, CardPool.CardContext cx)
    {
        view.title.text = cx.title;
        view.image.sprite = cx.image;
        view.description.text = cx.description;
    }

    bool IsSelected(CardView view)
    {
        return view.GetComponent<Selectable>()
                .state == Selectable.State.Selected;
    }
}
