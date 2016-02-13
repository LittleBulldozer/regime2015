using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using UnityEditorInternal;
using System.Linq;
using System.IO;

[CustomEditor(typeof(Trigger))]
public class TriggerEditor : Editor
{
	[MenuItem("Assets/Create/Trigger")]
	public static void CreateTrigger()
	{
		ScriptableObjectUtility.CreateAsset<Trigger>();
	}

	public void OnEnable()
	{
		condList = new ReorderableList(serializedObject,
			serializedObject.FindProperty("conditions"),
			true, true, true, true);
		condList.drawHeaderCallback = DrawHeaderCallback("Condition List");
		condList.onAddDropdownCallback = DropdownMenuCallback<Condition>(AddCondition);
		condList.drawElementCallback = DrawElementCallback(condList);
		condList.onSelectCallback = SelectCallback();
		condList.onRemoveCallback = RemoveCallback();

		actionList = new ReorderableList(serializedObject,
			serializedObject.FindProperty("actions"),
			true, true, true, true);
		actionList.drawHeaderCallback = DrawHeaderCallback("Action list");
		actionList.onAddDropdownCallback = DropdownMenuCallback<Action>(AddAction);
		actionList.drawElementCallback = DrawElementCallback(actionList);
		actionList.onSelectCallback = SelectCallback();
		actionList.onRemoveCallback = RemoveCallback();
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		condList.DoLayoutList();
		actionList.DoLayoutList();

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
	ReorderableList actionList;
	SerializedProperty selectedItem;
	Editor selectedItemEditor;

	void AddToList<T>(ReorderableList list, T item) where T : ScriptableObject
	{
		var originalPath = AssetDatabase.GetAssetPath(self);
		var _ref = originalPath.Split('.');
		var originalPathPure = _ref.Take(_ref.Length - 1).Aggregate((x,y) => x + "." + y);
		var actionListPath = originalPathPure + "_Meta.asset";
		var nativeAsset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(actionListPath);
		if (nativeAsset == null)
		{
			AssetDatabase.CreateAsset(item, actionListPath);
			AssetDatabase.SaveAssets ();
			AssetDatabase.Refresh();
		}
		else
		{
			AssetDatabase.AddObjectToAsset(item, nativeAsset);
		}
		//		AssetDatabase.AddObjectToAsset(item, desc);
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
			var newCond = ConditionFactory.Make(data.type);
			AddToList(condList, newCond);
		}
	}

	void AddAction(object userData)
	{
		ContextData data = userData as ContextData;
		if (data != null)
		{
			var newAction = ActionFactory.Make(data.type);
			AddToList(actionList, newAction);
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

	Trigger self
	{
		get {
			return target as Trigger;
		}
	}
}