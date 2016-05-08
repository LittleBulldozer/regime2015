using UnityEngine;
using System.Collections;

[Action("Popup", ActionGenere.SYSTEM)]
public class ScriptAction : Action, IUniqueID
{
	public int id = -1;

	public string script = "";

	public override void RunAction(int nrTurn)
	{
		ActionScriptDict.RunAction(id, nrTurn);
	}

	public int GetID()
	{
		return id;
	}
}
