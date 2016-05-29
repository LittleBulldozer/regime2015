using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(SplineWalker))]
public class SplineWalkerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        simulate = EditorGUILayout.Toggle("Simulate?", simulate);
        if (simulate)
        {
            EditorGUI.BeginChangeCheck();
            prog = EditorGUILayout.Slider(prog, 0f, 1f);
            if (EditorGUI.EndChangeCheck())
            {
                SplineWalker walker = (SplineWalker)target;
                walker.SetProgress(prog);
            }
        }

        DrawDefaultInspector();

        serializedObject.ApplyModifiedProperties();
    }

    float prog = 0;
    bool simulate = false;
}
