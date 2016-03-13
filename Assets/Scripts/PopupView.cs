using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopupView : MonoBehaviour
{
    public Text title;
    public Image image;
    public Text description;

    public void Set(string titleText, Sprite sprite, string desc)
    {
        title.text = titleText;
        image.sprite = sprite;
        description.text = desc;
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
