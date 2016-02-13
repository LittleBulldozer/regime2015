using UnityEngine;
using System.Collections;

[Action("Popup", ActionGenere.SYSTEM)]
public class ScriptAction : Action, IUniqueID
{
	public int id = -1;

	public string script = "";

	public override void RunAction()
	{
		ActionScriptDict.RunAction(id);
	}

	public int GetID()
	{
		return id;
	}
}
