using UnityEngine;
using System.Collections;

public class WorldNoteRef : MonoBehaviour
{
    public Canvas canvas;
    public Book book;
    public NoteUI noteUI;

    public void OpenPage(int pageIndex)
    {
        book.SetCurrentPageIndex(pageIndex);
        canvas.gameObject.SetActive(true);
    }
}
