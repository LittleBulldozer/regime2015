using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(SplineWalker))]
public class SplineWalkerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        walker.simulateInEditMode = EditorGUILayout.Toggle("Simulate?", walker.simulateInEditMode);

        if (walker.simulateInEditMode)
        {
            EditorGUI.BeginChangeCheck();
            prog = EditorGUILayout.Slider(prog, 0f, 1f);
            if (EditorGUI.EndChangeCheck())
            {
                walker.SetProgress(prog);
                EditorUtility.SetDirty(walker);
            }
        }

        DrawDefaultInspector();

        serializedObject.ApplyModifiedProperties();
    }

    float prog = 0;

    SplineWalker walker
    {
        get
        {
            return (SplineWalker)target;
        }
    }
}
