using UnityEngine;
using System.Collections;

public class GameFlow : MonoBehaviour
{
    public void NotifyCardSelected()
    {
        cardSelected = TheWorld.cardCanvas.selectedCard;
    }

    CardPool.CardContext cardSelected = null;
    RectTransform turnAlertPrefab;

    void Awake()
    {
        TheWorld.gameFlow = this;
        turnAlertPrefab = Resources.Load<RectTransform>("prefabs/TurnAlert");
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

            var turnAlert = Instantiate(turnAlertPrefab);
            turnAlert.SetParent(TheWorld.eventCanvas.transform, false);

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
