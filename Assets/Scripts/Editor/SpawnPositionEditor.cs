using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(SpawnPosition))]
public class SpawnPositionEditor : Editor
{
    GUIContent anchorTypeContent = new GUIContent("Anchor 종류");
    GUIContent customAnchorNameContent = new GUIContent("Custom Anchor 이름");
    GUIContent anchorRelationshipTypeContent = new GUIContent("Anchor와 관계");

    public void OnEnable()
    {
        obj = new SerializedObject(target);
        anchorType = obj.FindProperty("anchorType");
        customAnchorName = obj.FindProperty("customAnchorName");
        anchorRelationshipType = obj.FindProperty("anchorRelationshipType");
    }

    public override void OnInspectorGUI()
    {
        obj.Update();

        var spawnPos = target as SpawnPosition;
        
        if (spawnPos.GetAnchor() == null)
        {
            EditorGUILayout.HelpBox("Anchor를 찾지 못하였습니다.", UnityEditor.MessageType.Warning);
        }

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(anchorType, anchorTypeContent);
        if ((AnchorType)anchorType.intValue == AnchorType.CUSTOM)
        {
            EditorGUILayout.PropertyField(customAnchorName, customAnchorNameContent);
        }
        else
        {
            customAnchorName.stringValue = null;
        }

        EditorGUILayout.PropertyField(anchorRelationshipType, anchorRelationshipTypeContent);
        obj.ApplyModifiedProperties();

        bool propChanged = EditorGUI.EndChangeCheck();
        if (propChanged)
        {
            spawnPos.Reset();
            obj.Update();
        }

        if (spawnPos.GetAnchor() != null && GUILayout.Button("Anchor에 붙기"))
        {
            spawnPos.StickToAnchor();
            obj.Update();
        }

        obj.ApplyModifiedProperties();
    }

    SerializedObject obj;
    SerializedProperty anchorType;
    SerializedProperty customAnchorName;
    SerializedProperty anchorRelationshipType;
}
