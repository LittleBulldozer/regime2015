using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PageTemplateLoader), true)]
public class PageTemplateLoaderInspector : Editor
{
    public override void OnInspectorGUI()
    {
        PageTemplateLoader loader = target as PageTemplateLoader;

        var label = string.Format("Load {0} template", loader.nickname);
        if (GUILayout.Button(label))
        {
            loader.Load();
        }

        modifyToggle = EditorGUILayout.BeginToggleGroup("Modify", modifyToggle);
        if (modifyToggle)
        {
            DrawDefaultInspector();
        }
    }

    bool modifyToggle = false;
}
