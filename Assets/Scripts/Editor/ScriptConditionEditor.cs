using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;

[CustomEditor(typeof(ScriptCondition))]
public class ScriptConditionEditor : Editor
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

        if (GUILayout.Button("save"))
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.ImportAsset(ScriptConditionDict.singletonPath);
            AssetDatabase.Refresh();
        }
    }

    SerializedProperty script;
}
