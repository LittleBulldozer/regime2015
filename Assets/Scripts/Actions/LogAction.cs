using UnityEngine;
using System.Collections;

[Action("Log(Memo)", ActionGenere.SYSTEM)]
public class LogAction : Action
{
	public string memoText;

	public override void RunAction(int nrTurn)
	{
        Debug.Log("log action : " + memoText);
        var txt = string.Format("{0}턴 {1}", nrTurn + 1, memoText);
        TheWorld.logCenter.LeaveLog(new LogCenter.Log(txt));
	}
}
