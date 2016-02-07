using UnityEngine;
using System.Collections.Generic;

public class CardDesc : ScriptableObject
{
	public string title;
	public string nickname;
	public string description;
	public Sprite image;
	public float defaultPriority;

	public List<Action> actions = new List<Action>();
}
