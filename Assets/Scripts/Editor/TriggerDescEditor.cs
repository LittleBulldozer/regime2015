using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using UnityEditorInternal;

[CustomEditor(typeof(TriggerDesc))]
public class TriggerDescEditor : Editor
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
        condList.onChangedCallback = (ReorderableList list) => { UpdateName(); };

        eventList = new ReorderableList(serializedObject,
                serializedObject.FindProperty("events"),
                true, true, true, true);
        eventList.drawHeaderCallback = DrawHeaderCallback("이벤트 목록");
        eventList.onAddDropdownCallback = DropdownMenuCallback<Trigger.Event>(AddEvent);
        eventList.drawElementCallback = DrawElementCallback(eventList);
        eventList.onSelectCallback = SelectCallback();
        eventList.onRemoveCallback = RemoveCallback();
        eventList.onChangedCallback = (ReorderableList list) => { UpdateName(); };
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

        UpdateName();

        serializedObject.ApplyModifiedProperties();
    }

    class ContextData
    {
        public Type type;
    }
    
    ReorderableList condList;
    ReorderableList eventList;
    SerializedProperty selectedItem;
    Editor selectedItemEditor;

    void AddToList<T>(ReorderableList list, T item) where T : ScriptableObject
    {
        var trigger = target as TriggerDesc;
        AssetDatabase.AddObjectToAsset(item, trigger);
        var index = list.serializedProperty.arraySize;
        list.serializedProperty.arraySize++;
        list.index = index;
        var element = list.serializedProperty.GetArrayElementAtIndex(index);
        element.objectReferenceValue = item;
        serializedObject.ApplyModifiedProperties();
    }

    void AddCondition(object userData)
    {
        ContextData data = userData as ContextData;
        if (data != null)
        {
            var newCond = CreateInstance(data.type) as Trigger.Condition;
            newCond.name = data.type.ToString();
            AddToList(condList, newCond);
        }
    }

    void AddEvent(object userData)
    {
        ContextData data = userData as ContextData;
        if (data != null)
        {
            var newEvent = CreateInstance(data.type) as Trigger.Event;
            newEvent.name = data.type.ToString();
            AddToList(eventList, newEvent);
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

    string GetDescStringOfList(ReorderableList list, int limit)
    {
        var str = "";
        var arrayCount = list.serializedProperty.arraySize;
        for (int i = 0; i < arrayCount; i++)
        {
            var item = list.serializedProperty.GetArrayElementAtIndex(i);
            if (item != null && item.objectReferenceValue != null)
            {
                if (str.Length > 0)
                {
                    str += ", ";
                }
                str += item.objectReferenceValue.name;
            }
        }

        if (str.Length > limit)
        {
            str = str.Substring(0, 7) + "...";
        }

        return str;
    }

    void UpdateName()
    {
        var eventDesc = GetDescStringOfList(eventList, 40);
        if (eventDesc.Length == 0)
        {
            target.name = "Empty";
        }
        else
        {
            target.name = eventDesc;
        }
    }
}