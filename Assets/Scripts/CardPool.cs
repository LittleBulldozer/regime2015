﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CardPool : MonoBehaviour
{
	public class CardContext
	{
        public const float MAGIC_NUMBER = 100f;

		public CardContext(CardDesc desc)
		{
			this.desc = desc;
            ResetPriority();
        }

		public float defaultPriority
		{
			get
			{
				return desc.defaultPriority;
			}
		}

        public string title
        {
            get
            {
                return desc.title;
            }
        }

        public string nickname
        {
            get
            {
                return desc.nickname;
            }
        }

        public Sprite image
        {
            get
            {
                return desc.image;
            }
        }

        public string description
        {
            get
            {
                return desc.description;
            }
        }

        public string fullDescription
        {
            get
            {
                return desc.fullDescription;
            }
        }

        public void RunAction(int nrTurn)
        {
            foreach (var action in desc.actions)
            {
                action.RunAction(nrTurn);
            }
        }

        public float priority
        {
            get
            {
                return _priority;
            }

            set
            {
                _priority = value;
            }
        }

        public void ResetPriority()
        {
            _priority = desc.defaultPriority;
        }

		CardDesc desc;
        float _priority;
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
			cardContexts.Add(new CardContext(desc));
		}
	}

    public void TestDrawCards()
    {
        var cards = DrawCards(4);

        for (int i = 0; i < cards.Length; i++)
        {
            var card = cards[i];
            Debug.Log("card[" + i + "] : " + card.title);
        }
    }

    CardContext PopOne(List<CardContext> pool, float lowerLimit)
    {
        var filteredPool = pool.Where(x => x.priority >= lowerLimit);
        float total_weight = filteredPool.Sum(x => x.priority >= lowerLimit
            ? x.priority
            : 0);
        float random_value = Random.Range(0, total_weight);
        float probe = 0;
        var theOne = filteredPool.FirstOrDefault(x => {
            probe += x.priority;
            return probe > random_value;
        });
        if (theOne != null)
        {
            pool.Remove(theOne);
        }

        return theOne;
    }

    public CardContext [] DrawCards(int n)
    {
        var ret = new CardContext[n];

        var pool = new List<CardContext>(cardContexts);

        for (int i = 0; i < n; i++)
        {
            var privilegedOne = PopOne(pool, CardContext.MAGIC_NUMBER);
            if (privilegedOne != null)
            {
                ret[i] = privilegedOne;
                continue;
            }

            var theOne = PopOne(pool, 0);
            if (theOne == null)
            {
                throw new System.Exception("Logical Exception!");
            }
            ret[i] = theOne;
        }

        return ret;
    }

    public CardPoolScriptController GetController()
	{
		return new CardPoolScriptController(this);
	}

    public CardContext GetCard(string nickname)
    {
        return cardContexts.Find(x => x.nickname == nickname);
    }

	List<CardContext> cardContexts = new List<CardContext>();
}
