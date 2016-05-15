using UnityEngine;
using System.Collections;
using UnityEditor;

//[CanEditMultipleObjects]
//[CustomEditor(typeof(Book), true)]
public class BookInspector : Editor
{
    public override void OnInspectorGUI()
    {
        /*
        Book book = target as Book;

        if (GUILayout.Button("Save"))
        {
            book.StoreCachedContentsInfo();
        }

        if (GUILayout.Button("Clear"))
        {
            book.MoveShownContents(book.hiddenContents);
        }

        EditorGUI.BeginChangeCheck();
        var editorPageIndex = EditorGUILayout.IntField("Page Index", book.currentPageIndex);

        if (EditorGUI.EndChangeCheck())
        {
            if (book.IsDirty())
            {
                var msg = string.Format("Would you like to save page {0}?", book.GetCachedPageIndex());
                int ret = EditorUtility.DisplayDialogComplex("Save Page?", msg, "Yes", "No", "Cancel");
                if (ret == 0)
                {
                    // Yes
                    book.currentPageIndex = editorPageIndex;
                    EditorUtility.SetDirty(target);
                }
                else if (ret == 1)
                {
                    // No
                    book.currentPageIndex = editorPageIndex;
                    book.SkipStoreInfo();
                    EditorUtility.SetDirty(target);
                }
                else if (ret == 2)
                {
                    // Cancel

                }
            }
            else
            {
                book.currentPageIndex = editorPageIndex;
                EditorUtility.SetDirty(target);
            }
        }*/

        DrawDefaultInspector();
    }
}
