using UnityEngine;
using System.Collections;

public class ScriptCondition : Condition, IUniqueID
{
	public int id = -1;

	public string script = "true";

	public override bool IsSatisfied ()
	{
//		return 
		return false;
	}

	public int GetID()
	{
		return id;
	}
}
