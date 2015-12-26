using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]
public class PageBGAmender : MonoBehaviour
{
    public Sprite bgSprite;

    Image cachedPage;
    Sprite cachedBGSprite;

    Image GetPage()
    {
        if (transform.parent == null || transform.parent.parent == null)
        {
            return null;
        }

        return transform.parent.parent.GetComponent<Image>();
    }

	void OnEnable()
    {
        cachedPage = GetPage();
        if (cachedPage == null)
        {
            return;
        }

        cachedBGSprite = cachedPage.sprite;
        if (bgSprite != null)
        {
            cachedPage.sprite = bgSprite;
        }
    }
	
	void OnDisable ()
    {
        if (cachedPage == null)
        {
            return;
        }

        cachedPage.sprite = cachedBGSprite;
    }
}
