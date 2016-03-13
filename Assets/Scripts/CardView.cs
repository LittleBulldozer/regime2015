using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    public Text title;
    public Image image;
    public Text description;

    public void SetCardContext(CardPool.CardContext cx)
    {
        this.cx = cx;

        title.text = cx.title;
        image.sprite = cx.image;
        description.text = cx.description;
    }

    CardPool.CardContext cx;
}
