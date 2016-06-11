using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(CardAnimParam))]
public class CardView : MonoBehaviour
{
    public CardPool.CardContext cx;
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

    public IEnumerator GoAway()
    {
        yield return new WaitForSeconds(0.5f);
    }

    CardAnimParam cardAnimParam;

    void Awake()
    {
        cardAnimParam = GetComponent<CardAnimParam>();
    }

    void OnEnable()
    {
        cardAnimParam.yogi = 0f;
    }
}
