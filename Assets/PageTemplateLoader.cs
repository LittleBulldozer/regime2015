using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Book))]
public class PageTemplateLoader : MonoBehaviour
{
    public List<GameObject> objs;
    public string nickname;

    public void Load()
    {
        var book = GetComponent<Book>();
        // clear
        book.MoveShownContents(book.hiddenContents);

        // load
        foreach (var obj in objs)
        {
            obj.transform.SetParent(book.shownContents, false);
        }
    }
}
