using UnityEngine;
using System.Collections;

public class TabLook : MonoBehaviour
{
    public int targetPageIndex;
    public RectTransform originalContainer;
    public RectTransform advancedContainer;
    public Book theBook;

    void Awake()
    {
        theBook.preloadEvent += PagePreloaded;
        theBook.switchEvent += PageSwitched;
    }

    void OnDestroy()
    {
        theBook.switchEvent -= PageSwitched;
        theBook.preloadEvent -= PagePreloaded;
    }

    public void PagePreloaded(bool inverse)
    {
        var RT = GetComponent<RectTransform>();

        if (targetPageIndex == theBook.currentPageIndex)
        {
            

            if (inverse)
            {
                RT.SetParent(theBook.frontSpace, false);
            }
            else
            {
                RT.SetParent(advancedContainer, false);
            }
        }
        else
        {
            RT.SetParent(originalContainer, false);
        }
	}

    public void PageSwitched()
    {
        var RT = GetComponent<RectTransform>();

        if (targetPageIndex == theBook.currentPageIndex)
        {
            var originalPos = RT.anchoredPosition;
            transform.SetParent(theBook.frontSpace, false);
            StartCoroutine(KeepPos(originalPos));
        }
        else
        {
            RT.SetParent(originalContainer, false);
        }
    }

    IEnumerator KeepPos(Vector3 pos)
    {
        var RT = GetComponent<RectTransform>();
        int count = 5;
        while (count-- > 0)
        {
            RT.anchoredPosition = pos;
            yield return null;
        }
    }
}
