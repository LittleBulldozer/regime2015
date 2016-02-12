using UnityEngine;
using System.Collections.Generic;

public class CardPool : MonoBehaviour
{
	public class CardContext
	{
		public CardContext(CardDesc desc)
		{
			this.desc = desc;
			priority = desc.defaultPriority;
		}

		public float defaultPriority
		{
			get
			{
				return desc.defaultPriority;
			}
		}

		public float priority;

		CardDesc desc;
	}

	public class CardPoolScriptController
	{
		public CardPoolScriptController(CardPool self)
		{
			this.self = self;
		}

		CardPool self;
	}

	public void Awake()
	{
		var cardDescs = Resources.LoadAll<CardDesc>("cards");
		foreach (var desc in cardDescs)
		{
			Debug.Log(desc.name);
			cardContexts.Add(new CardContext(desc));
		}
	}

	public CardPoolScriptController GetController()
	{
		return new CardPoolScriptController(this);
	}

	List<CardContext> cardContexts = new List<CardContext>();
}
