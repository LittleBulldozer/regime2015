using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;

[CustomEditor(typeof(ScriptAction))]
public class ScriptActionEditor : Editor
{
	public void OnEnable()
	{	
		script = serializedObject.FindProperty("script");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUI.BeginChangeCheck();
		script.stringValue = EditorGUILayout.TextArea(script.stringValue);
		serializedObject.ApplyModifiedProperties();
		if (EditorGUI.EndChangeCheck())
		{
			AssetDatabase.ImportAsset(ScriptActionDict.singletonPath);
		}
	}


	SerializedProperty script;
}
