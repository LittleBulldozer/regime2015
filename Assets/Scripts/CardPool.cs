using UnityEngine;
using System.Collections.Generic;

using UniLinq;


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

        public string title
        {
            get
            {
                return desc.title;
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

        public void RunAction()
        {
            foreach (var action in desc.actions)
            {
                action.RunAction();
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

    public CardContext [] DrawCards(int n)
    {
        var ret = new CardContext[n];

        var pool = new List<CardContext>(cardContexts);

        for (int i = 0; i < n; i++)
        {
            float total_weight = pool.Sum(x => x.priority);
            float random_value = Random.Range(0, total_weight);
            float probe = 0;
            var theOne = pool.Find(x => {
                probe += x.priority;
                return probe > random_value;
            });
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

	List<CardContext> cardContexts = new List<CardContext>();
}
