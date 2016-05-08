using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TriggerMgr : MonoBehaviour
{
    [System.NonSerialized]
    public List<Trigger> list;

    public void RunAllTriggers(int nrTurn)
    {
        foreach (var trigger in list)
        {
            if (trigger.conditions.All(cond => cond.IsSatisfied(nrTurn)))
            {
                foreach (var action in trigger.actions)
                {
                    action.RunAction(nrTurn);
                }
            }
        }
    }

	void Awake ()
    {
        list = new List<Trigger>(Resources.LoadAll<Trigger>("triggers"));
	}
}
