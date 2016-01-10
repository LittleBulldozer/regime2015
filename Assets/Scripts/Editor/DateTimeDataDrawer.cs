using UnityEngine;
using UnityEditor;
using System;

[CustomPropertyDrawer(typeof(DateTimeData))]
public class GameDateTimeDrawer : PropertyDrawer
{
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        Rect yearRect = new Rect(position.x, position.y, 50, position.height);
        Rect monthRect = new Rect(position.x + 55, position.y, 30, position.height);
        Rect dayRect = new Rect(position.x + 90, position.y, 30, position.height);

        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(yearRect, property.FindPropertyRelative("year"), GUIContent.none);
        EditorGUI.PropertyField(monthRect, property.FindPropertyRelative("month"), GUIContent.none);
        EditorGUI.PropertyField(dayRect, property.FindPropertyRelative("day"), GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
