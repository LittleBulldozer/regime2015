using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Linq;
using UnityEditorInternal;

[CustomEditor(typeof(CardDesc))]
public class CardDescEditor : Editor
{
	[MenuItem("Assets/Create/Card")]
	public static void CreateCardDesc()
	{
		ScriptableObjectUtility.CreateAsset<CardDesc>();
	}

	public void OnEnable()
	{
		actionList = new ReorderableList(serializedObject
			, serializedObject.FindProperty("actions")
			, true, true, true, true);
		actionList.drawHeaderCallback = DrawHeaderCallback("Action List");
		actionList.onAddDropdownCallback = DropdownMenuCallback<Action>(AddAction);
		actionList.drawElementCallback = DrawElementCallback(actionList);
		actionList.onSelectCallback = SelectCallback();
		actionList.onRemoveCallback = RemoveCallback();

		infoStyle = new GUIStyle();
		infoStyle.normal.textColor = Color.white;
		cardTemplateTex = Resources.Load<Texture2D>("NewMagicTemplate");

		titleStyle = new GUIStyle();
		descStyle = new GUIStyle();
		descStyle.wordWrap = true;
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUILayout.PropertyField(serializedObject.FindProperty("title"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("nickname"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("description"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("image"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("defaultPriority"));

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

	public override bool HasPreviewGUI()
	{
		return true;
	}

	public override void OnInteractivePreviewGUI(Rect rt, GUIStyle bg)
	{
		var auto = new LayoutHelper(300, 400, rt);
		EditorGUI.DrawPreviewTexture(auto.Rect(0, 0, 300, 400), cardTemplateTex);
		if (desc.image != null)
		{
			var spriteEditor = Editor.CreateEditor(desc.image, null);
			var tex = spriteEditor.RenderStaticPreview("", null, 250, 200);
			EditorGUI.DrawPreviewTexture(auto.Rect(25, 78, 250, 168), tex);
		}
		titleStyle.fontSize = (int)(auto.scale * 18);
		EditorGUI.LabelField(auto.Rect(25, 30, 250, 50), desc.title, titleStyle);
		descStyle.fontSize = (int)(auto.scale * 15);
		EditorGUI.LabelField(auto.Rect(25, 280, 250, 150), desc.description, descStyle);
	}

	ReorderableList actionList;
	SerializedProperty selectedItem;
	Editor selectedItemEditor;
	GUIStyle infoStyle;
	GUIStyle titleStyle;
	GUIStyle descStyle;
	Texture cardTemplateTex;

	CardDesc desc
	{
		get
		{
			return target as CardDesc;
		}
	}

	delegate Rect Rector();
	delegate Vector2 Pointer(Vector2 point);

	class LayoutHelper
	{
		public float scale;

		public LayoutHelper(float W, float H, Rect rt)
		{
			this.W = W;
			this.H = H;
			this.rt = rt;

			var standardRatio = (float)H / W;
			var realRatio = rt.height / rt.width;

			scale = realRatio > standardRatio ?
				rt.width / W // // sharp device
				: rt.height / H;  //blunt device
		}

		public Rect Rect(float x, float y, float width, float height)
		{
			var p0 = new Vector2(x, y);
			var p1 = new Vector2(x + width, y + height);
			p0 = TransformPoint(p0);
			p1 = TransformPoint(p1);

			return new Rect(rt.x + p0.x, rt.y + p0.y, p1.x - p0.x, p1.y - p0.y);	
		}

		float W;
		float H;
		Rect rt;

		Vector2 CentralizePoint(Vector2 p)
		{
			return new Vector2(p.x - 0.5f * W, p.y - 0.5f * H);
		}

		Vector2 ScalePoint(Vector2 p)
		{
			return new Vector2(p.x * scale, p.y * scale);
		}

		Vector2 DecentralizedPoint(Vector2 p)
		{
			return new Vector2(p.x + 0.5f * rt.width, p.y + 0.5f * rt.height);
		}

		Vector2 TransformPoint(Vector2 p)
		{
			return DecentralizedPoint(ScalePoint(CentralizePoint(p)));
		}
	}

	class ContextData
	{
		public Type type;
	}

	void AddToList<T>(ReorderableList list, T item) where T : ScriptableObject
	{
		var originalPath = AssetDatabase.GetAssetPath(desc);
		var _ref = originalPath.Split('.');
		var originalPathPure = _ref.Take(_ref.Length - 1).Aggregate((x,y) => x + "." + y);
		var actionListPath = originalPathPure + "_Actions.asset";
		var nativeActionAsset = AssetDatabase.LoadAssetAtPath<Action>(actionListPath);
		if (nativeActionAsset == null)
		{
			AssetDatabase.CreateAsset(item, actionListPath);
			AssetDatabase.SaveAssets ();
			AssetDatabase.Refresh();
		}
		else
		{
			AssetDatabase.AddObjectToAsset(item, nativeActionAsset);
		}
//		AssetDatabase.AddObjectToAsset(item, desc);
		var index = list.serializedProperty.arraySize;
		list.serializedProperty.arraySize++;
		list.index = index;
		var element = list.serializedProperty.GetArrayElementAtIndex(index);
		element.objectReferenceValue = item;
		serializedObject.ApplyModifiedProperties();
	}

	void AddAction(object userData)
	{
		ContextData data = userData as ContextData;
		if (data != null)
		{
			var newAction = CreateInstance(data.type) as Action;
			newAction.name = data.type.ToString();
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
				DestroyImmediate(element.objectReferenceValue, true);
				prop.DeleteArrayElementAtIndex(list.index);
			}

			prop.DeleteArrayElementAtIndex(list.index);
		};
	}
}
