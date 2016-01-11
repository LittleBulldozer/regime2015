using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

[CustomEditor(typeof(ActionBlock)), CanEditMultipleObjects]
public class ActionBlockEditor : Editor
{
    GUIContent addTriggerButtonContent = new GUIContent("Trigger 추가");

    public void OnEnable()
    {
        obj = new SerializedObject(target);
    }

    public override void OnInspectorGUI()
    {
        var actionBlock = target as ActionBlock;

        obj.Update();

        DrawDefaultInspector();

        if (GUILayout.Button(addTriggerButtonContent))
        {
            var newTrigger = new GameObject("Empty Trigger");
            newTrigger.AddComponent<Trigger>();
            newTrigger.transform.SetParent(actionBlock.transform, false);
            Selection.activeGameObject = newTrigger;
        }

        obj.ApplyModifiedProperties();
    }

    SerializedObject obj;
}