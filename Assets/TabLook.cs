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

        PageSwitched();
    }

    void OnDestroy()
    {
        theBook.switchEvent -= PageSwitched;
        theBook.preloadEvent -= PagePreloaded;
    }

	void PagePreloaded(bool inverse)
    {
        var RT = GetComponent<RectTransform>();

        if (targetPageIndex == theBook.currentPageIndex)
        {
            

            if (inverse)
            {
                RT.SetParent(theBook.shownContents, false);
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

    void PageSwitched()
    {
        if (targetPageIndex == theBook.currentPageIndex)
        {
            var RT = GetComponent<RectTransform>();
            var originalPos = RT.anchoredPosition;
            transform.SetParent(theBook.shownContents, false);
            StartCoroutine(KeepPos(originalPos));
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
