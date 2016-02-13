using UnityEngine;
using System.Collections.Generic;

public class Trigger : ScriptableObject
{
	public List<Condition> conditions = new List<Condition>();
	public List<Action> actions = new List<Action>();
}
