using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Action)), CanEditMultipleObjects]
public class ActionEditor : Editor
{
    public void OnEnable()
    {
        actor = serializedObject.FindProperty("actor");
    }

    public override void OnInspectorGUI()
    {
        var action = target as Action;

        serializedObject.Update();

        EditorGUILayout.PropertyField(actor, actorContent);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Blocks", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;

        action.CacheBlocks();
        var blocks = action.blocks;

        foreach (var B in blocks)
        {
            EditorGUILayout.BeginHorizontal();
            var label = string.Format("({0} secs) {1}", B.duration, B.name);
            EditorGUILayout.LabelField(label);
            if (GUILayout.Button(inspectorIcon, GUILayout.Width(24)))
            {
                Selection.activeObject = B;
            }
            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Add Block"))
        {
            var newBlock = new GameObject(string.Format("Block {0}", blocks.Count));
            newBlock.AddComponent<ActionBlock>();
            newBlock.transform.SetParent(action.transform, false);
        }

        EditorGUI.indentLevel--;

        serializedObject.ApplyModifiedProperties();
    }

    bool containerFoldout = false;

    GUIContent actorContent = new GUIContent("Actor");
    GUIContent blocksContent = new GUIContent("Blocks");
    GUIContent durationContent = new GUIContent("Duration");

    Texture inspectorIcon = null;
    
    SerializedProperty actor;

    void Awake()
    {
        if (inspectorIcon == null)
        {
            inspectorIcon = Resources.Load<Texture>("Director_InspectorIcon");
        }
    }
}
