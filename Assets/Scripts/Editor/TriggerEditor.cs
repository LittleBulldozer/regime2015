using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using UnityEditorInternal;

[CustomEditor(typeof(Trigger))]
public class TriggerEditor : Editor
{
    public void OnEnable()
    {
        condList = new ReorderableList(serializedObject,
                serializedObject.FindProperty("conditions"),
                true, true, true, true);
        condList.drawHeaderCallback = DrawHeaderCallback("조건 목록");
        condList.onAddDropdownCallback = DropdownMenuCallback<Trigger.Condition>(AddCondition);
        condList.drawElementCallback = DrawElementCallback(condList);
        condList.onSelectCallback = SelectCallback();
        condList.onRemoveCallback = RemoveCallback();

        eventList = new ReorderableList(serializedObject,
                serializedObject.FindProperty("events"),
                true, true, true, true);
        eventList.drawHeaderCallback = DrawHeaderCallback("이벤트 목록");
        eventList.onAddDropdownCallback = DropdownMenuCallback<Trigger.Event>(AddEvent);
        eventList.drawElementCallback = DrawElementCallback(eventList);
        eventList.onSelectCallback = SelectCallback();
        eventList.onRemoveCallback = RemoveCallback();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        condList.DoLayoutList();

        eventList.DoLayoutList();

        if (selectedItem != null && selectedItem.objectReferenceValue != null)
        {
            var label = string.Format("{0}", selectedItem.objectReferenceValue.GetType().FullName);
            EditorGUILayout.LabelField(label, EditorStyles.boldLabel);

            selectedItemEditor.OnInspectorGUI();
            EditorGUILayout.Space();
        }

        serializedObject.ApplyModifiedProperties();
    }

    class ContextData
    {
        public Type type;
    }
    
    ReorderableList condList;
    ReorderableList eventList;
    GUIContent addCondButtonContent = new GUIContent("조건 추가");
    GUIContent addEventButtonContent = new GUIContent("이벤트 추가");
    SerializedProperty selectedItem;
    Editor selectedItemEditor;
    
    void AddCondition(object userData)
    {
        ContextData data = userData as ContextData;
        if (data != null)
        {
            var newCond = CreateInstance(data.type) as Trigger.Condition;
            var trigger = target as Trigger;
            trigger._Conditions.Add(newCond);
        }
    }

    void AddEvent(object userData)
    {
        ContextData data = userData as ContextData;
        if (data != null)
        {
            var newEvent = CreateInstance(data.type) as Trigger.Event;
            var trigger = target as Trigger;
            trigger._Events.Add(newEvent);
        }
    }
    
    ReorderableList.HeaderCallbackDelegate DrawHeaderCallback(string label)
    {
        return rect =>
        {
            EditorGUI.LabelField(rect, label);
        };
    }

    ReorderableList.AddDropdownCallbackDelegate DropdownMenuCallback<T>(System.Action<object> OnAdd)
    {
        return (Rect buttonRect, ReorderableList list) =>
        {
            GenericMenu createMenu = new GenericMenu();

            foreach (var type in TypeHelper.GetAllSubTypes(typeof(T)))
            {
                ContextData userData = new ContextData { type = type };
                createMenu.AddItem(new GUIContent(type.ToString()), false, new GenericMenu.MenuFunction2(OnAdd), userData);
            }

            createMenu.ShowAsContext();
        };
    }

    ReorderableList.ElementCallbackDelegate DrawElementCallback(ReorderableList list)
    {
        return (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var element = list.serializedProperty.GetArrayElementAtIndex(index);
            if (element.objectReferenceValue != null)
            {
                EditorGUI.LabelField(rect, element.objectReferenceValue.GetType().FullName);
            }
        };
    }

    ReorderableList.SelectCallbackDelegate SelectCallback()
    {
        return list =>
        {
            var element = list.serializedProperty.GetArrayElementAtIndex(list.index);
            if (element.objectReferenceValue != null)
            {
                selectedItem = element;
                selectedItemEditor = CreateEditor(element.objectReferenceValue);
            }
        };
    }

    ReorderableList.RemoveCallbackDelegate RemoveCallback()
    {
        return list =>
        {
            var prop = list.serializedProperty;
            var element = prop.GetArrayElementAtIndex(list.index);
            if (selectedItem != null && selectedItem.objectReferenceValue == element.objectReferenceValue)
            {
                selectedItem = null;
                selectedItemEditor = null;
            }

            if (element.objectReferenceValue != null)
            {
                prop.DeleteArrayElementAtIndex(list.index);
            }

            prop.DeleteArrayElementAtIndex(list.index);
        };
    }
}