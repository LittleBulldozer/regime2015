using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TextNews : MonoBehaviour
{
    public Text scrollableText;
    public string text;
    public float scrollSpeed = 10.0f;

    Queue<string> queue = new Queue<string>();

	// Use this for initialization
	void Start ()
    {
        queue.Enqueue(text);

        StartCoroutine(MainLoop());
	}

    IEnumerator MainLoop()
    {
        while (true)
        {
            if (queue.Count == 0)
            {
                yield return new WaitForSeconds(1.0f);
            }

            var t = queue.Dequeue();
            yield return StartCoroutine(Show(t));
        }
    }

    IEnumerator Show(string t)
    {
        scrollableText.text = t;
        var rectTransform = GetComponent<RectTransform>();

        scrollableText.rectTransform.anchoredPosition =
            new Vector2(rectTransform.rect.width / 2.0f, scrollableText.rectTransform.anchoredPosition.y);

        while (scrollableText.rectTransform.rect.x + scrollableText.rectTransform.rect.width > -rectTransform.rect.width / 2.0f)
        {
            var pos = scrollableText.rectTransform.anchoredPosition;
            scrollableText.rectTransform.anchoredPosition =
                new Vector2(pos.x - scrollSpeed * Time.deltaTime, pos.y);

            yield return null;
        }
    }
}
