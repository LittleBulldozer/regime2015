using UnityEngine;
using System.Collections;

public class GameFlow : MonoBehaviour
{
    public void NotifyCardSelected()
    {
        cardSelected = TheWorld.cardCanvas.selectedCard;
    }

    CardPool.CardContext cardSelected = null;

    void Awake()
    {
        TheWorld.gameFlow = this;
    }

    void Start ()
    {
        StartCoroutine(MainFlow());
	}
	
    IEnumerator MainFlow()
    {
        while (true)
        {
            Debug.Log("Beginning of the turn.");

            var contexts = TheWorld.cardPool.DrawCards(4);
            TheWorld.cardCanvas.ApplyView(contexts);

            while (true)
            {
                if (cardSelected != null)
                {
                    cardSelected.RunAction();
                    cardSelected = null;
                    break;
                }

                yield return null;
            }
        }
    }
}
