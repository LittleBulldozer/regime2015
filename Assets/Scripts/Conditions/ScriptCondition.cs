using UnityEngine;
using System.Collections;

public class ScriptCondition : Condition, IUniqueID
{
	public int id = -1;

	public string script = "true";

	public override bool IsSatisfied (int nrTurn)
	{
        return ConditionScriptDict.TestCondition(id, nrTurn);
	}

	public int GetID()
	{
		return id;
	}
}
