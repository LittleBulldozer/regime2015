using UnityEngine;
using UnityEditor;
using System;

public class ConditionFactory
{
	public static Condition Make(Type t)
	{
		var newCond = ScriptableObject.CreateInstance(t) as Condition;
		if (t == typeof(ScriptCondition))
		{
			var scriptCond = newCond as ScriptCondition;
			var dict = ScriptConditionDict.singleton;
			scriptCond.id = dict.GetNextId();
			dict.items.Add(scriptCond);
			EditorUtility.SetDirty(dict);
		}
		newCond.name = t.ToString();
		return newCond;
	}
}