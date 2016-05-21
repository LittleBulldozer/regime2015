using UnityEngine;
using System.Collections;

public class CardCanvas : MonoBehaviour
{
    public CardView cardLT;
    public CardView cardRT;
    public CardView cardLB;
    public CardView cardRB;
    public CardCanvasScaleGroup scaleGroup;

    public CardPool.CardContext [] contexts;

    public CardPool.CardContext selectedCard
    {
        get {
            if (IsSelected(cardLT))
            {
                return cardLT.cx;
            }

            if (IsSelected(cardRT))
            {
                return cardRT.cx;
            }

            if (IsSelected(cardLB))
            {
                return cardLB.cx;
            }

            if (IsSelected(cardRB))
            {
                return cardRB.cx;
            }

            return null;
        }
    }

    public void ApplyView(CardPool.CardContext [] contexts)
    {
        this.contexts = contexts;

        cardLT.SetCardContext(contexts[0]);
        cardRT.SetCardContext(contexts[1]);
        cardLB.SetCardContext(contexts[2]);
        cardRB.SetCardContext(contexts[3]);
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
