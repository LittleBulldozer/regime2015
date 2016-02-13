using UnityEngine;
using UnityEditor;
using System;

public class ActionFactory
{
	public static Action Make(Type t)
	{
		var newAction = ScriptableObject.CreateInstance(t) as Action;
		if (t == typeof(ScriptAction))
		{
			var scriptAction = newAction as ScriptAction;
			var dict = ScriptActionDict.singleton;
			scriptAction.id = dict.GetNextId();
			dict.items.Add(scriptAction);
			EditorUtility.SetDirty(dict);
		}
		newAction.name = t.ToString();
		return newAction;
	}
}