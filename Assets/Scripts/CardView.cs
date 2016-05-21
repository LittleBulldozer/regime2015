using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    public CardPool.CardContext cx;
    public Text title;
    public Image image;
    public Text description;
    public Animator anchoredAnim;

    public void SetCardContext(CardPool.CardContext cx)
    {
        this.cx = cx;

        title.text = cx.title;
        image.sprite = cx.image;
        description.text = cx.description;
        anchoredAnim.SetTrigger("Idle");
    }

    public IEnumerator GoAway()
    {
        anchoredAnim.SetTrigger("GoAway");

        yield return new WaitForSeconds(0.5f);
    }
}
