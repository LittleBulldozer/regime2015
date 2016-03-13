using UnityEngine;
using System.Collections;

public class GameFlow : MonoBehaviour
{
    public CardCanvas cardCanvas;

    public void NotifyCardSelected()
    {
        cardSelected = cardCanvas.selectedCard;
    }

    CardPool.CardContext cardSelected = null;

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
            cardCanvas.ApplyView(contexts);

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
